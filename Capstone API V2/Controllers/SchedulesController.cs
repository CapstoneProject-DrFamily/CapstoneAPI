using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Capstone_API_V2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(DateTime startDate, DateTime endDate, int doctorId)
        {
            var result = await _scheduleService.GetAll(filter: f => f.AppointmentTime >= startDate && f.AppointmentTime <= endDate && f.DoctorId ==  doctorId && f.Disabled == false, 
                orderBy: o => o.OrderBy(s => s.AppointmentTime), 
                includeProperties: "Transactions,Transactions.Doctor,Doctor.DoctorNavigation,Transactions.Patient,Transactions.Patient.PatientNavigation,Transactions.Service")
                .ToListAsync();
            foreach(var schedule in result)
            {
                if(schedule.Transactions.Count > 0)
                {
                    var docId = schedule.DoctorId;
                    schedule.Transactions = (from t in schedule.Transactions select t).Where(t => t.Status != Constants.TransactionStatus.CANCEL && t.ScheduleId == schedule.ScheduleId).ToList();
                    if(schedule.Transactions.Count > 0)
                    {
                        var patientId = schedule.Transactions.First().PatientId.GetValueOrDefault();
                        schedule.Transactions.First().isOldPatient = _scheduleService.checkIsOldPatient(docId, patientId);
                    }
                }
            }
            return Ok(result);
        }

        [HttpGet("Checking")]
        public async Task<IActionResult> CheckingSchedule(int doctorId)
        {
            var result = await _scheduleService.isCheckingTransaction(doctorId);
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var schedules = await _scheduleService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, filter: f => f.Disabled == false, orderBy: o => o.OrderBy(d => d.AppointmentTime));
            var result = new
            {
                schedules,
                schedules.TotalPages,
                schedules.HasPreviousPage,
                schedules.HasNextPage
            };
            return Ok(result);
        }

        /*[HttpGet("Patients/{patientId}/Upcoming")]
        public async Task<IActionResult> GetUpcomingSchedule([FromRoute]int patientId, [FromQuery] ResourceParameter model)
        {
            var schedules = await _scheduleService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,
                filter: f => f.AppointmentTime >= _scheduleService.ConvertTimeZone() && f.Disabled == false && f.Status == true && f.Transactions.SingleOrDefault().PatientId == patientId,
                includeProperties: "Doctor,Doctor.DoctorNavigation,Doctor.Specialty,Transactions,Transactions.Service",
                orderBy: o => o.OrderBy(d => d.AppointmentTime));
            var result = new
            {
                schedules,
                schedules.TotalPages,
                schedules.HasPreviousPage,
                schedules.HasNextPage
            };
            return Ok(result);
        }*/

        [HttpGet("Patients/{patientId}/Upcoming")]
        public async Task<IActionResult> GetUpcomingSchedule([FromRoute]int patientId)
        {
            var lstSchedule = await _scheduleService.GetAll(filter: f => f.AppointmentTime >= _scheduleService.ConvertTimeZone() && f.Disabled == false && f.Status == true && f.Transactions.SingleOrDefault().PatientId == patientId).ToListAsync();
            List<ScheduleModel> result = new List<ScheduleModel>();
            foreach(var schedule in lstSchedule)
            {
                if(schedule.Transactions.Any(t => t.Status == Constants.TransactionStatus.OPEN))
                {
                    var transactions = schedule.Transactions.Where(t => t.Status == Constants.TransactionStatus.OPEN).ToList();
                    schedule.Transactions = transactions;
                    result.Add(schedule);
                }
            }
            return Ok(result);
        }

        [HttpGet("Patients/{patientId}/Overtime")]
        public async Task<IActionResult> GetOvertimeSchedule([FromRoute]int patientId, [FromQuery] ResourceParameter model)
        {
            var schedules = await _scheduleService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,
                filter: f => f.AppointmentTime < _scheduleService.ConvertTimeZone() && f.Disabled == false && f.Status == true && f.Transactions.SingleOrDefault().Status == 0 && f.Transactions.SingleOrDefault().PatientId == patientId,
                includeProperties: "Doctor,Doctor.DoctorNavigation,Doctor.Specialty,Transactions,Transactions.Service",
                orderBy: o => o.OrderBy(d => d.AppointmentTime));
            var result = new
            {
                schedules,
                schedules.TotalPages,
                schedules.HasPreviousPage,
                schedules.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{scheduleId}")]
        public async Task<IActionResult> GetById(int scheduleId)
        {
            var result = await _scheduleService.GetByIdAsync(scheduleId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<ScheduleSimpModel> model)
        {
            var result = await _scheduleService.CreateScheduleAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{scheduleId}")]
        public async Task<IActionResult> Delete(int scheduleId)
        {
            var result = await _scheduleService.DeleteAsync(scheduleId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ScheduleSimpModel model)
        {
            var result = await _scheduleService.UpdateScheduleAsync(model);
            /*if(result != null && result.Status == true)
            {
                TwilioClient.Init(Constants.SMSConfig.USERNAME, Constants.SMSConfig.PASSWORD);
                //var toNumber = Constants.SMSConfig.VN_REGION_CODE + _scheduleService.GetPhoneNumber(result.DoctorId);
                //var toNumber = "+840932958412";
                var toNumber = "+840866492081";

                var message = MessageResource.Create(
                    body: Constants.SMSConfig.BODY_TEMPLATE + " " + result.AppointmentTime.ToString() + " from" + " " + result.InsBy + "!",
                    from: new Twilio.Types.PhoneNumber(Constants.SMSConfig.SERVER_NUMER),
                    to: new Twilio.Types.PhoneNumber(toNumber)
                );
            }*/
            return Ok(result);
        }
    }
}