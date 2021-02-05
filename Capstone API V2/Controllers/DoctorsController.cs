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
            var result = await _doctorService.GetAll(includeProperties: "Profile").ToListAsync();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var result = await _doctorService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize);
            //var result = await _patientService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, filter: f => f.Disabled == false);
            return Ok(result);
        }

        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetById(int doctorId)
        {
            var result = await _doctorService.GetByIdAsync(doctorId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DoctorModel model)
        {
            var result = await _doctorService.CreateAsync(model);
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
        public async Task<IActionResult> Update([FromBody] DoctorModel model)
        {
            var result = await _doctorService.UpdateAsync(model);
            return Ok(result);
        }
    }
}