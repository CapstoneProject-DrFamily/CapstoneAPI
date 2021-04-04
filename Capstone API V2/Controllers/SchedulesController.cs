using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                includeProperties: "ScheduleNavigation,ScheduleNavigation.Doctor,Doctor.DoctorNavigation,ScheduleNavigation.Patient,ScheduleNavigation.Patient.PatientNavigation,ScheduleNavigation.Service")
                .ToListAsync();
            foreach(var schedule in result)
            {
                var docId = schedule.DoctorId.GetValueOrDefault();
                var patientId = schedule.ScheduleNavigation.PatientId.GetValueOrDefault();

                schedule.ScheduleNavigation.isOldPatient = _scheduleService.checkIsOldPatient(docId, patientId);
            }
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

        [HttpGet("Patients/{patientId}/Upcoming")]
        public async Task<IActionResult> GetUpcomingSchedule([FromRoute]int patientId, [FromQuery] ResourceParameter model)
        {
            var schedules = await _scheduleService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,
                filter: f => _scheduleService.ConvertTimeZone() >= f.AppointmentTime && f.Disabled == false && f.Status == true && f.ScheduleNavigation.Status == 0 && f.ScheduleNavigation.PatientId == patientId,
                includeProperties: "Doctor,Doctor.DoctorNavigation,Doctor.Specialty,ScheduleNavigation,ScheduleNavigation.Service",
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

        [HttpGet("Patients/{patientId}/Overtime")]
        public async Task<IActionResult> GetOvertimeSchedule([FromRoute]int patientId, [FromQuery] ResourceParameter model)
        {
            var schedules = await _scheduleService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,
                filter: f => _scheduleService.ConvertTimeZone() < f.AppointmentTime && f.Disabled == false && f.Status == true && f.ScheduleNavigation.Status == 0 && f.ScheduleNavigation.PatientId == patientId,
                includeProperties: "Doctor,Doctor.DoctorNavigation,Doctor.Specialty,ScheduleNavigation,ScheduleNavigation.Service",
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
        public async Task<IActionResult> GetById(string scheduleId)
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
        public async Task<IActionResult> Delete(string scheduleId)
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
            return Ok(result);
        }
    }
}