﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Helper;
using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Capstone_API_V2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppConfigsController : ControllerBase
    {
        private readonly IAppConfigService _appConfigService;
        public AppConfigsController(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appConfigService.GetAll().ToListAsync();

            //var result = await _diseaseService.GetAllDisease();
            return Ok(result);
        }

        [HttpGet("{appId}")]
        public async Task<IActionResult> GetById([FromRoute]int appId)
        {
            var appConfigModel = await _appConfigService.GetAll(filter: f => f.AppId == appId).SingleOrDefaultAsync();
            if(appConfigModel != null && appId == Constants.Roles.DOCTOR_APP_ID)
            {
                DoctorAppConfigModel result = new DoctorAppConfigModel();

                if (!string.IsNullOrEmpty(appConfigModel.ConfigValue))
                {
                    var jObject = JsonConvert.DeserializeObject<JObject>(appConfigModel.ConfigValue);
                    var timeout = jObject.GetValue("timeout");
                    var prsTemplate = jObject.GetValue("prescriptionTemplates");
                    var policy = jObject.GetValue("policy");
                    var appointmentNotifyTime = jObject.GetValue("appointmentNotifyTime");
                    var imageQuantity = jObject.GetValue("imageQuantity");
                    var minusTimeInMinute = jObject.GetValue("minusTimeInMinute");
                    var examinationHour = jObject.GetValue("examinationHour");

                    result.Timeout = timeout.ToObject<int>();
                    result.PrescriptionTemplates = prsTemplate.ToObject<Dictionary<string, PrescriptionSimpModel>>();
                    result.Policy = policy.ToObject<string>();
                    result.AppointmentNotifyTime = appointmentNotifyTime.ToObject<int>();
                    result.ImageQuantity = imageQuantity.ToObject<int>();
                    result.MinusTimeInMinute = minusTimeInMinute.ToObject<int>();
                    result.ExaminationHour = examinationHour.ToObject<double>();
                }

                result.AppId = appConfigModel.AppId;
                result.AppName = appConfigModel.AppName;

                return Ok(result);
            }

            if (appConfigModel != null && appId == Constants.Roles.PATIENT_APP_ID)
            {
                PatientAppConfigModel result = new PatientAppConfigModel();

                if (!string.IsNullOrEmpty(appConfigModel.ConfigValue))
                {
                    var jObject = JsonConvert.DeserializeObject<JObject>(appConfigModel.ConfigValue);
                    var relationships = jObject.GetValue("relationships");
                    var distances = jObject.GetValue("distances");
                    var policy = jObject.GetValue("policy");

                    result.RelationShips = relationships.ToObject<List<string>>();
                    result.Distances = distances.ToObject<List<int>>();
                    result.Policy = policy.ToObject<string>();
                }

                result.AppId = appConfigModel.AppId;
                result.AppName = appConfigModel.AppName;

                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var appConfigs = await _appConfigService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize);
            var result = new
            {
                appConfigs,
                appConfigs.TotalPages,
                appConfigs.HasPreviousPage,
                appConfigs.HasNextPage
            };
            return Ok(result);
        }

        /*[HttpGet("{appId}")]
        public async Task<IActionResult> GetById(int appId)
        {
            var result = await _appConfigService.GetByIdAsync(appId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }*/

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppConfigModel model)
        {
            var result = await _appConfigService.CreateAsync(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{appId}")]
        public async Task<IActionResult> Delete(int appId)
        {
            var result = await _appConfigService.DeleteAsync(appId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut("DoctorApp")]
        public async Task<IActionResult> UpdateDoctorApp([FromBody] DoctorAppConfigModel model)
        {
            JObject @object = new JObject();
            @object.Add("timeout", JToken.FromObject(model.Timeout));
            @object.Add("prescriptionTemplates", JToken.FromObject(model.PrescriptionTemplates));
            @object.Add("policy", JToken.FromObject(model.Policy));
            @object.Add("appointmentNotifyTime", JToken.FromObject(model.AppointmentNotifyTime));
            @object.Add("imageQuantity", JToken.FromObject(model.ImageQuantity));
            @object.Add("minusTimeInMinute", JToken.FromObject(model.MinusTimeInMinute));
            @object.Add("examinationHour", JToken.FromObject(model.ExaminationHour));

            var configValue = JsonConvert.SerializeObject(@object);

            AppConfigModel appConfigModel = new AppConfigModel();
            appConfigModel.AppId = model.AppId;
            appConfigModel.AppName = model.AppName;
            appConfigModel.ConfigValue = configValue;

            var result = await _appConfigService.UpdateAsync(appConfigModel);
            return Ok(result);
        }

        [HttpPut("PatientApp")]
        public async Task<IActionResult> UpdatePatientApp([FromBody] PatientAppConfigModel model)
        {
            JObject @object = new JObject();
            @object.Add("relationships", JToken.FromObject(model.RelationShips));
            @object.Add("distances", JToken.FromObject(model.Distances));
            @object.Add("policy", JToken.FromObject(model.Policy));

            var configValue = JsonConvert.SerializeObject(@object);

            AppConfigModel appConfigModel = new AppConfigModel();
            appConfigModel.AppId = model.AppId;
            appConfigModel.AppName = model.AppName;
            appConfigModel.ConfigValue = configValue;

            var result = await _appConfigService.UpdateAsync(appConfigModel);
            return Ok(result);
        }
    }
}