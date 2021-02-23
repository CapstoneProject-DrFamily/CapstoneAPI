using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone_API_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IFamilyDoctorService _familyDoctorService;
        public ServicesController(IFamilyDoctorService familyDoctorService)
        {
            _familyDoctorService = familyDoctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _familyDoctorService.GetAll(filter: f => f.Disabled == false).ToListAsync();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var services = await _familyDoctorService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,
                filter: service => !string.IsNullOrWhiteSpace(model.SearchValue) ? service.Disabled == false
                && service.ServiceName.StartsWith(model.SearchValue) : service.Disabled == false,
                orderBy: o => o.OrderBy(s => s.ServiceName));
            var result = new
            {
                services,
                services.TotalPages,
                services.HasPreviousPage,
                services.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{serviceId}")]
        public async Task<IActionResult> GetById(int serviceId)
        {
            var result = await _familyDoctorService.GetByIdAsync(serviceId);
            if (result == null || result.Disabled == true)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceModel model)
        {
            var result = await _familyDoctorService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{serviceId}")]
        public async Task<IActionResult> Delete(int serviceId)
        {
            var result = await _familyDoctorService.DeleteAsync(serviceId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ServiceModel model)
        {
            var result = await _familyDoctorService.UpdateAsync(model);
            return Ok(result);
        }
    }
}