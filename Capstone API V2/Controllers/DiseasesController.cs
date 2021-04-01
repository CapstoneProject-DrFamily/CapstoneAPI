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
    public class DiseasesController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;
        public DiseasesController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _diseaseService.GetAll(filter: f => f.Disabled == false).Take(1000).ToListAsync();

            //var result = await _diseaseService.GetAllDisease();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var diseases = await _diseaseService.GetAsync(model);
            var result = new
            {
                diseases,
                diseases.TotalPages,
                diseases.HasPreviousPage,
                diseases.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{diseaseId}")]
        public async Task<IActionResult> GetById(string diseaseId)
        {
            var result = await _diseaseService.GetDiseaseByID(diseaseId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DiseaseModel model)
        {
            var result = await _diseaseService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{diseaseId}")]
        public async Task<IActionResult> Delete(string diseaseId)
        {
            var result = await _diseaseService.DeleteAsync(diseaseId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DiseaseModel model)
        {
            var result = await _diseaseService.UpdateAsync(model);
            return Ok(result);
        }
    }
}