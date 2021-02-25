using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_API_V2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PresciptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        public PresciptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _prescriptionService.GetAllPrescription();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var prescriptions = await _prescriptionService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, includeProperties: "PrescriptionDetails");

            var result = new
            {
                prescriptions,
                prescriptions.TotalPages,
                prescriptions.HasPreviousPage,
                prescriptions.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{prescriptionId}")]
        public async Task<IActionResult> GetById(int prescriptionId)
        {
            var result = await _prescriptionService.GetPrescriptionByID(prescriptionId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrescriptionSimpModel model)
        {
            var result = await _prescriptionService.CreatePrescription(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{prescriptionId}")]
        public async Task<IActionResult> Delete(int prescriptionId)
        {
            var result = await _prescriptionService.DeleteAsync(prescriptionId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PrescriptionSimpModel model)
        {
            var result = await _prescriptionService.UpdatePrescription(model);
            return Ok(result);
        }
    }
}