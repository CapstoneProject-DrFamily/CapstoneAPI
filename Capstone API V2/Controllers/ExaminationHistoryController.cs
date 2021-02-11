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
    public class ExaminationHistoryController : ControllerBase
    {
        private readonly IExaminationHistoryService _examinationHistoryService;
        public ExaminationHistoryController(IExaminationHistoryService examinationHistoryService)
        {
            _examinationHistoryService = examinationHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _examinationHistoryService.GetAll().ToListAsync();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var result = await _examinationHistoryService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize);
            return Ok(result);
        }

        [HttpGet("{examinationId}")]
        public async Task<IActionResult> GetById(int examinationId)
        {
            var result = await _examinationHistoryService.GetByIdAsync(examinationId);
            if (result == null || result.Disabled == true)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExaminationHistoryModel model)
        {
            var result = await _examinationHistoryService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{examinationId}")]
        public async Task<IActionResult> Delete(int examinationId)
        {
            var result = await _examinationHistoryService.DeleteAsync(examinationId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ExaminationHistoryModel model)
        {
            var result = await _examinationHistoryService.UpdateAsync(model);
            return Ok(result);
        }
    }
}