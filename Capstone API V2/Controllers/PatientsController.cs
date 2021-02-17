﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone_API_V2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _patientService.GetAll().ToListAsync();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var result = await _patientService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize);
            //var result = await _patientService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, filter: f => f.Disabled == false);
            return Ok(result);
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetById(int patientId)
        {
            var result = await _patientService.GetByIdAsync(patientId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientSimpModel model)
        {
            var result = await _patientService.CreatePatient(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{patientId}")]
        public async Task<IActionResult> Delete(int patientId)
        {
            var result = await _patientService.DeleteAsync(patientId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PatientSimpModel model)
        {
            var result = await _patientService.UpdatePatient(model);
            return Ok(result);
        }

        [HttpGet("{accountId}/Dependents")]
        public async Task<IActionResult> GetDepdent(int accountId)
        {
            var result = await _patientService.GetDepdentByIdAsync(accountId).ToListAsync();
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}