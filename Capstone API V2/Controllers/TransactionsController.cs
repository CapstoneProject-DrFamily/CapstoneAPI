using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Helper;
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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _transactionService.GetAllTransaction();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var transactions = await _transactionService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, 
                filter: transaction => !string.IsNullOrWhiteSpace(model.SearchValue) ? transaction.TransactionId.Equals(model.SearchValue)
                && transaction.Disabled == false : transaction.Disabled == false && transaction.Status != 0, 
                includeProperties: "Doctor,Doctor.Specialty,Doctor.Profile,Exam,Patient,Patient.Profile,Prescription,Service,SymptomDetails,SymptomDetails.Symptom");

            var result = new
            {
                transactions,
                transactions.TotalPages,
                transactions.HasPreviousPage,
                transactions.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetById(string transactionId)
        {
            var result = await _transactionService.GetTransactionByID(transactionId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("doctors/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId(int doctorId,[Required] int status)
        {
            var result = await _transactionService.GetTransactionByDoctorIDAsync(doctorId, status).ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("patients/{patientId}")]
        public async Task<IActionResult> GetByPatientId(int patientId,[Required] int status)
        {
            var result = await _transactionService.GetTransactionByPatientIDAsync(patientId, status).ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionSimpModel model)
        {
            var result = await _transactionService.CreateTransaction(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{transactionId}")]
        public async Task<IActionResult> Delete(string transactionId)
        {
            var result = await _transactionService.DeleteAsync(transactionId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TransactionPutModel model)
        {
            var result = await _transactionService.UpdateTransaction(model);
            return Ok(result);
        }
    }
}