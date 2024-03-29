﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class HealthRecordsController : ControllerBase
    {
        private readonly IHealthRecordService _healthRecordService;
        public HealthRecordsController(IHealthRecordService healthRecordService)
        {
            _healthRecordService = healthRecordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([Required]int patientId, bool isOldRecord)
        {
            if (isOldRecord)
            {
                var result = await _healthRecordService.GetAll(filter: f => f.PatientId == patientId && f.Disable == true).ToListAsync();
                return Ok(result);
            }
            return Ok(await _healthRecordService.GetAll(filter: f => f.PatientId == patientId && f.Disable == false).ToListAsync());
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var healthRecords = await _healthRecordService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize);
            //var result = await _patientService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, filter: f => f.Disabled == false);
            var result = new
            {
                healthRecords,
                healthRecords.TotalPages,
                healthRecords.HasPreviousPage,
                healthRecords.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{healthRecordId}")]
        public async Task<IActionResult> GetById(int healthRecordId)
        {
            var result = await _healthRecordService.GetByIdAsync(healthRecordId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HealthRecordModel model)
        {
            var result = await _healthRecordService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{healthRecordId}")]
        public async Task<IActionResult> Delete(int healthRecordId)
        {
            var result = await _healthRecordService.DeleteAsync(healthRecordId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] HealthRecordModel model)
        {
            var result = await _healthRecordService.UpdateAsync(model);
            return Ok(result);
        }
    }
}