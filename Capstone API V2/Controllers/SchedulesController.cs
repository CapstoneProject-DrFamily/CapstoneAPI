using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
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
            var result = await _scheduleService.GetAll(filter: f => f.AppointmentTime >= startDate && f.AppointmentTime <= endDate && f.DoctorId ==  doctorId && f.Disabled == false, orderBy: o => o.OrderBy(s => s.AppointmentTime)).ToListAsync();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var feedbacks = await _scheduleService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, filter: f => f.Disabled == false, orderBy: o => o.OrderBy(d => d.AppointmentTime));
            var result = new
            {
                feedbacks,
                feedbacks.TotalPages,
                feedbacks.HasPreviousPage,
                feedbacks.HasNextPage
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
        public async Task<IActionResult> Create([FromBody] ScheduleModel model)
        {
            var result = await _scheduleService.CreateAsync(model);
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
        public async Task<IActionResult> Update([FromBody] ScheduleModel model)
        {
            var result = await _scheduleService.UpdateAsync(model);
            return Ok(result);
        }
    }
}