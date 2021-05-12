using Capstone_API_V2.Helper;
using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SpecialtiesController : Controller
    {
        private readonly ISpecialtyService _specialtyService;
        public SpecialtiesController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _specialtyService.GetAll(filter: f => f.Disabled == false, includeProperties: "Services").ToListAsync();
            foreach (var specialty in result)
            {
                specialty.Services = (from service in specialty.Services select service).Where(service => service.Disabled == false).ToList();
            }
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var specialties = await _specialtyService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,
                filter: specialty => !string.IsNullOrWhiteSpace(model.SearchValue) ? specialty.Disabled == false
                && specialty.Name.StartsWith(model.SearchValue) : specialty.Disabled == false,
                orderBy: o => o.OrderBy(d => d.Name));
            var result = new
            {
                specialties,
                specialties.TotalPages,
                specialties.HasPreviousPage,
                specialties.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{specialtyId}")]
        public async Task<IActionResult> GetById(int specialtyId)
        {
            var result = await _specialtyService.GetAll(filter: f => f.Id == specialtyId && f.Disabled == false, includeProperties: "Services").SingleOrDefaultAsync();
            if (string.IsNullOrEmpty(result.Name))
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SpecialtyModel model)
        {
            var result = await _specialtyService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{specialtyId}")]
        public async Task<IActionResult> Delete(int specialtyId)
        {
            var result = await _specialtyService.DeleteAsync(specialtyId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SpecialtyModel model)
        {
            var result = await _specialtyService.UpdateAsync(model);
            return Ok(result);
        }
    }
}
