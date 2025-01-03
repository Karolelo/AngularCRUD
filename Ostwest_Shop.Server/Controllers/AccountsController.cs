using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Ostwest_Shop.Server.DTOs;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;
using Ostwest_Shop.Server.Services;

namespace Ostwest_Shop.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly UserServices _userServices;
    private readonly IConfiguration _configuration;
    
    public AccountsController(UserServices userServices,IConfiguration configuration)
    {
        _userServices = userServices;
        _configuration = configuration;
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<User>> CreateAccount([FromBody]CreateUserDto user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var addedUser = await _userServices.CreateUser(user);
        return Ok(addedUser);
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var isUserValid = await _userServices.CheckUser(loginRequest.Username, loginRequest.Password);
        if (!isUserValid)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }
        
        var user = await _userServices.GetUserByUsername(loginRequest.Username);
        if (user == null)
        {
            return Unauthorized(new { message = "User not found" });
        }
        
        string role = user.Username=="admin" ? "admin" : "user";
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username), 
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, role) 
        };
    
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtIssuerOptions:key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtIssuerOptions:Issuer"],
            audience: _configuration["JwtIssuerOptions:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30), 
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new
        {
            token = tokenString,
            role = role
        });
    }
    
}