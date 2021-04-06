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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SymptomsController : ControllerBase
    {
        private readonly ISymptomService _symptomService;
        public SymptomsController(ISymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _symptomService.GetAll(filter: f => f.Disabled == false).ToListAsync();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var symptoms = await _symptomService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,
                filter: symptom => !string.IsNullOrWhiteSpace(model.SearchValue) ? symptom.Disabled == false
                && symptom.Name.StartsWith(model.SearchValue) : symptom.Disabled == false,
                orderBy: o => o.OrderBy(d => d.Name));
            var result = new
            {
                symptoms,
                symptoms.TotalPages,
                symptoms.HasPreviousPage,
                symptoms.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{symptomId}")]
        public async Task<IActionResult> GetById(int symptomId)
        {
            var result = await _symptomService.GetByIdAsync(symptomId);
            if (result == null || result.Disabled == true)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SymptomModel model)
        {
            var result = await _symptomService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{symptomId}")]
        public async Task<IActionResult> Delete(int symptomId)
        {
            var result = await _symptomService.DeleteAsync(symptomId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SymptomModel model)
        {
            var result = await _symptomService.UpdateAsync(model);
            return Ok(result);
        }
    }
}