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
    [Route("api/[controller]")]
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
            var result = await _profileService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize);
            return Ok(result);
        }

        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetById(int profileId)
        {
            var result = await _profileService.GetByIdAsync(profileId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProfileModel model)
        {
            var result = await _profileService.CreateAsync(model);
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
        public async Task<IActionResult> Update([FromBody] ProfileModel model)
        {
            var result = await _profileService.UpdateAsync(model);
            return Ok(result);
        }
    }
}