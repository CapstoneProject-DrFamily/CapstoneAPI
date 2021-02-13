﻿using System;
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
    [Route("api/[controller]")]
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
            //var result = await _transactionService.GetAll(filter: transaction => transaction.Disabled == false, includeProperties: "SymptomDetails,Symtoms").ToListAsync();
            var result = await _transactionService.GetAllTransaction();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            byte transactionStatus = byte.Parse(model.SearchValue);

            var result = await _transactionService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, filter: transaction => transaction.Status == transactionStatus, includeProperties: "SymptomDetails");
            return Ok(result);
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetById(string transactionId)
        {
            //var result = await _transactionService.GetAll(filter: transaction => transaction.TransactionId == transactionId && transaction.Disabled == false, includeProperties: "SymptomDetails").ToListAsync();
            //var result = await _transactionService.GetByIdAsync(transactionId);

            var result = await _transactionService.GetTransactionByID(transactionId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionModel model)
        {
            var result = await _transactionService.CreateAsync(model);
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
        public async Task<IActionResult> Update([FromBody] TransactionModel model)
        {
            var result = await _transactionService.UpdateAsync(model);
            return Ok(result);
        }
    }
}