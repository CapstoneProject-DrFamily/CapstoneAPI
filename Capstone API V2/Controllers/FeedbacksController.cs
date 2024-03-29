﻿using System;
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
            var feedbacks = await _feedbackService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,  
                filter: f => !string.IsNullOrWhiteSpace(model.SearchValue) ? f.Id.Equals(model.SearchValue) : !f.Id.Equals(""), 
                orderBy: o => o.OrderByDescending(feedback => feedback.InsDatetime));
            var result = new
            {
                feedbacks,
                feedbacks.TotalPages,
                feedbacks.HasPreviousPage,
                feedbacks.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("paging/Doctors")]
        public async Task<IActionResult> GetByDoctor([FromQuery] ResourceParameter model)
        {
            var feedbacks = await _feedbackService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, 
                filter: f => f.IdNavigation.DoctorId == int.Parse(model.SearchValue), 
                orderBy: o => o.OrderByDescending(feedback => feedback.InsDatetime));
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
        public async Task<IActionResult> GetById(string feedbackId)
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
        public async Task<IActionResult> Delete(string feedbackId)
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