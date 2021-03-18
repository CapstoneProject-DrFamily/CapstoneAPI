using Capstone_API_V2.Services;
using Capstone_API_V2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_API_V2.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IAuthenticatedService _authService;

        private readonly IConfiguration _config;

        public AuthController(IUserService userService, IRoleService roleService, IConfiguration config, IAuthenticatedService authService)
        {
            _userService = userService;
            _roleService = roleService;
            _authService = authService;
            _config = config;
        }

        /*[HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _userService.CreateUser(model, model.Password);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }*/

        [HttpPost("OTP")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _authService.LoginOTP(model);
            if (user != null)
            {
                var role = model.RoleID;
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var firebaseProject = _config.GetSection("AppSettings:FirebaseProject").Value;
                var token = new JwtSecurityToken(
                    issuer: "https://securetoken.google.com/" + firebaseProject,
                    audience: firebaseProject,
                    expires: DateTime.Now.AddYears(13),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    userId = user.AccountId,
                    phone = model.PhoneNumber,
                    role = role,
                    //profileId = user.ProfileId,
                    /*email = user.Email,
                    fullName = user.FullName,
                    username = user.Username,
                    photo = user.Photo,*/
                    expiration = token.ValidTo,
                    waiting = user.Waiting,
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginAdminModel model)
        {
            var user = await _userService.GetByUserName(model.Username);
            var result = await _userService.CheckPassWord(model.Username, model.Password);
            if (user != null && result)
            {
                var role = _roleService.GetRole(user);
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var firebaseProject = _config.GetSection("AppSettings:FirebaseProject").Value;
                var token = new JwtSecurityToken(
                    issuer: "https://securetoken.google.com/" + firebaseProject,
                    audience: firebaseProject,
                    expires: DateTime.Now.AddYears(13),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    username = model.Username,
                    role = role,
                    /*email = user.Email,
                    fullName = user.FullName,
                    username = user.Username,
                    photo = user.Photo,*/
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}