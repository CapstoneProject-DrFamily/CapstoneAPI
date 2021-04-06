using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Capstone_API_V2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SymptomsController : ControllerBase
    {
        private readonly ISymptomService _symptomService;
        public SymptomsController(ISymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _symptomService.GetAll(filter: f => f.Disabled == false).ToListAsync();
            
            var sendGridClient = new SendGridClient("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");
            /*string toEmail = "nguyenphu036@gmail.com";
            var from = new EmailAddress("taitpse130083@fpt.edu.vn", "AAA");
            var subject = "Accept Doctor";
            var to = new EmailAddress(toEmail, "BBB");
            var plainContent = "Doctor is accepted";
            var htmlContent = "<h1>Hello Doctor</h1>";*/

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom("taitpse130083@fpt.edu.vn", "FamilyDoctor");
            sendGridMessage.AddTo("nguyenphu036@gmail.com", "Tai");
            sendGridMessage.SetTemplateId("d-6fcc2d8f17f54a12927dddfe30c8776b");
            sendGridMessage.SetTemplateData(new EmailConfig
            {
                Name = "Phu Tai"
            });

            //var mailMessage = MailHelper.CreateSingleEmail(from, to, subject, plainContent, htmlContent);

            var res = await sendGridClient.SendEmailAsync(sendGridMessage);

            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var symptoms = await _symptomService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,
                filter: symptom => !string.IsNullOrWhiteSpace(model.SearchValue) ? symptom.Disabled == false
                && symptom.Name.StartsWith(model.SearchValue) : symptom.Disabled == false,
                orderBy: o => o.OrderBy(d => d.Name));
            var result = new
            {
                symptoms,
                symptoms.TotalPages,
                symptoms.HasPreviousPage,
                symptoms.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{symptomId}")]
        public async Task<IActionResult> GetById(int symptomId)
        {
            var result = await _symptomService.GetByIdAsync(symptomId);
            if (result == null || result.Disabled == true)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SymptomModel model)
        {
            var result = await _symptomService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{symptomId}")]
        public async Task<IActionResult> Delete(int symptomId)
        {
            var result = await _symptomService.DeleteAsync(symptomId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SymptomModel model)
        {
            var result = await _symptomService.UpdateAsync(model);
            return Ok(result);
        }

        private class EmailConfig
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}