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

namespace Capstone_API_V2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var result = await _doctorService.GetAll(includeProperties: "Specialty,Profile").ToListAsync();
            var result = await _doctorService.GetAllDoctor();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var doctors = await _doctorService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, 
                filter: f => !string.IsNullOrWhiteSpace(model.SearchValue) ? f.DoctorNavigation.FullName.Contains(model.SearchValue) 
                && f.Disabled == false
                && f.DoctorNavigation.Account.Disabled == false : f.Disabled == false 
                && f.DoctorNavigation.Account.Disabled == false,
                includeProperties: "Specialty,DoctorNavigation,DoctorNavigation.Account");
            var result = new
            {
                doctors,
                doctors.TotalPages,
                doctors.HasPreviousPage,
                doctors.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetById(int doctorId)
        {
            var result = await _doctorService.GetDoctorByID(doctorId);
            
            //var result = await _doctorService.GetAll(filter: doctor => doctor.DoctorId == doctorId, includeProperties: "Specialty,Profile").ToListAsync();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("specialties/{specialtyId}")]
        public async Task<IActionResult> GetDoctorBySpecialtyId([FromRoute]int specialtyId, [FromQuery]ResourceParameter model)
        {
            var doctors = await _doctorService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,
                filter: f => f.SpecialtyId == specialtyId && f.Disabled == false && f.DoctorNavigation.Account.Disabled == false && f.Schedules.Single().Status == false,
                includeProperties: "Specialty,DoctorNavigation,DoctorNavigation.Account,Schedules", orderBy: o => o.OrderBy(s => s.Schedules.SingleOrDefault().AppointmentTime));

            foreach (var doctor in doctors)
            {
                doctor.Schedules = doctor.Schedules.OrderBy(s => s.AppointmentTime).ToList();
            }


            var result = new
            {
                doctors,
                doctors.TotalPages,
                doctors.HasPreviousPage,
                doctors.HasNextPage
            };
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DoctorSimpModel model)
        {
            var result = await _doctorService.CreateDoctor(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{doctorId}")]
        public async Task<IActionResult> Delete(int doctorId)
        {
            var result = await _doctorService.DeleteAsync(doctorId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DoctorSimpModel model)
        {
            var result = await _doctorService.UpdateDoctor(model);
            return Ok(result);
        }

        [HttpGet("{profileId}/SimpleInfo")]
        public async Task<IActionResult> GetInfoRequest(int profileId)
        {
            var result = await _doctorService.GetRequestDoctorInfo(profileId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("waitingList")]
        public async Task<IActionResult> GetWaitingDoctor()
        {
            var result = await _doctorService.GetWaitingDoctor();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}