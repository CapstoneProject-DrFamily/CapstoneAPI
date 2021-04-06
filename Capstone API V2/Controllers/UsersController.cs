using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_API_V2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        /*[HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameter model)
        {
            var result = await _userService.GetAsync(pageIndex: model.PageIndex, pageSize: model.PageSize,
                filter: user => user.Disabled == false);
            return Ok(result);
        }*/

        /*[HttpGet("{account/username}")]
        public async Task<IActionResult> GetById(string username)
        {
            //var result = await _userService.GetByIdAsync(medicineId);
            var result = await _userService.GetByUserName(username);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }*/

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetById(int accountId)
        {
            var result = await _userService.GetByIdAsync(accountId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel model)
        {
            var result = await _userService.CreateUser(model);
            if (result != null)
            {
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> Delete(int accountId)
        {
            var result = await _userService.DeleteUser(accountId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserModel model)
        {
            var result = await _userService.UpdateUser(model);

            var doctor = await _userService.GetAsync(0,0,filter: f => f.AccountId == model.AccountId, includeProperties: "Profiles");

            await _userService.SendEmailAsync(doctor.SingleOrDefault().Profiles.SingleOrDefault().Email, doctor.SingleOrDefault().Profiles.SingleOrDefault().FullName, model.Waiting, model.Disabled);
            return Ok(result);
        }
    }
}