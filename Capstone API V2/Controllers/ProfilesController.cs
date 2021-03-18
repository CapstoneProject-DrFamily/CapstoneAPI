using System;
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
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;
        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _profileService.GetAll().ToListAsync();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var profiles = await _profileService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize, 
                filter: f => !string.IsNullOrWhiteSpace(model.SearchValue) ? f.FullName.Contains(model.SearchValue) : f.ProfileId != 0);
            var result = new
            {
                profiles,
                profiles.TotalPages,
                profiles.HasPreviousPage,
                profiles.HasNextPage
            };
            return Ok(result);
        }

        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetById(int profileId)
        {
            var result = await _profileService.GetAll(filter: f => f.ProfileId == profileId , includeProperties: "Account,Patient,Doctor").SingleOrDefaultAsync();
            if (result != null)
            {
                if (result.Doctor != null)
                {
                    if (result.Doctor.Disabled == false)
                    {
                        return Ok(result);
                    }
                }
                if (result.Patient != null)
                {
                    if (result.Patient.Disabled == false)
                    {
                        return Ok(result);
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProfileSimpModel model)
        {
            var result = await _profileService.CreateProfile(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{profileId}")]
        public async Task<IActionResult> Delete(int profileId)
        {
            var result = await _profileService.DeleteAsync(profileId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProfileSimpModel model)
        {
            var result = await _profileService.UpdateProfile(model);
            return Ok(result);
        }
    }
}