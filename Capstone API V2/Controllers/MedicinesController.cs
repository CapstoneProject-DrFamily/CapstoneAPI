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
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        public MedicinesController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicineService.GetAll(filter: f => f.Disabled == false).Take(1000).ToListAsync();
            return Ok(result);
        }

        /*[HttpGet("{drugName}")]
        public async Task<IActionResult> GetByName(string drugName)
        {
            var result = await _drugService.GetByDrugName(drugName);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }*/

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            //var result = await _medicineService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, filter: f => f.Disabled == false, orderBy: o => o.OrderBy(d => d.Name));
            var result = await _medicineService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, filter: f => f.Disabled == false);
            return Ok(result);
        }

        [HttpGet("{medicineId}")]
        public async Task<IActionResult> GetById(int medicineId)
        {
            var result = await _medicineService.GetByIdAsync(medicineId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicineModel model)
        {
            var result = await _medicineService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{medicineId}")]
        public async Task<IActionResult> Delete(string medicineId)
        {
            var result = await _medicineService.DeleteAsync(medicineId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MedicineModel model)
        {
            var result = await _medicineService.UpdateAsync(model);
            return Ok(result);
        }
    }
}
