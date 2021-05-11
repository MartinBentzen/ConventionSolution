using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.Interfaces;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.MarvelConvention.Dto;

namespace WebApi.MarvelConvention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IConfiguration _config;

        public UserManagementController(IUserManagementService userManagementService, IConfiguration config)
        {
            _userManagementService = userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
            _config = config;
        }
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]UserDto userDto)
        {
            Address address = userDto.AddressDto == null ? null : Address.Create(userDto.AddressDto.City, userDto.AddressDto.Road, userDto.AddressDto.Number, userDto.AddressDto.PostalCode);
            var user = Domain.Model.User.Create(Guid.NewGuid(), userDto.Name, userDto.Email, userDto.Password, address, userDto.Role);
            await _userManagementService.Create(user);
            return Ok();
        }
        [HttpPost]    
        [AllowAnonymous]  
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest login)    
        {    
            IActionResult response = Unauthorized();    
            User user = await _userManagementService.AuthenticateUser(login.Email, login.Password);    
            if (user != null)    
            {    
                var tokenString = GenerateJWT(user);    
                response = Ok(new    
                {    
                    token = tokenString,    
                    userDetails = user,    
                });    
            }    
            return response;    
        }
          
        private string GenerateJWT(User userInfo)    
        {    
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
            var claims = new[]    
            {    
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),    
                new Claim("userId", userInfo.Id.ToString()),    
                new Claim("role",userInfo.Role),    
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),    
            };
           
            var token = new JwtSecurityToken(    
                issuer: _config["Jwt:Issuer"],    
                audience: _config["Jwt:Audience"],    
                claims: claims,    
                expires: DateTime.Now.AddMinutes(30),    
                signingCredentials: credentials    
            );    
            return new JwtSecurityTokenHandler().WriteToken(token);    
        }    


    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}