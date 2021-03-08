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
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _feedbackService.GetAll().ToListAsync();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var feedbacks = await _feedbackService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize);
            var result = new
            {
                feedbacks,
                feedbacks.TotalPages,
                feedbacks.HasPreviousPage,
                feedbacks.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{feedbackId}")]
        public async Task<IActionResult> GetById(int feedbackId)
        {
            var result = await _feedbackService.GetByIdAsync(feedbackId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FeedbackModel model)
        {
            var result = await _feedbackService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{feedbackId}")]
        public async Task<IActionResult> Delete(int feedbackId)
        {
            var result = await _feedbackService.DeleteAsync(feedbackId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FeedbackModel model)
        {
            var result = await _feedbackService.UpdateAsync(model);
            return Ok(result);
        }
    }
}