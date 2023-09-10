using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private ISelfAuthenticationService selfAuthenticationService { get; }

    public AuthController(ISelfAuthenticationService selfAuthenticationService)
    {
        this.selfAuthenticationService = selfAuthenticationService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto userDto)
    {
        // normally use repostiry pattern to handle register
        var existUser = await selfAuthenticationService.GetAsync(userDto);
        if (existUser != null)
        {
            return BadRequest($"{userDto.UserName} already registered");
        }
        var user = await selfAuthenticationService.RegisterAsync(userDto);
        return Ok(user);
    }
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto userDto)
    {
        // normally use repostiry pattern to handle register
        var existUser = await selfAuthenticationService.GetAsync(userDto);
        if (existUser == null)
        {
            return NotFound($"{userDto.UserName} was not found.");
        }
        var token = await selfAuthenticationService.LoginAsync(userDto);
        if (token == String.Empty)
        {
            return BadRequest($"invalid password");
        }
        return Ok(token);
    }
}
