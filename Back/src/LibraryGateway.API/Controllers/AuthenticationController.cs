using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryGateway.Application.Interfaces;
using LibraryGateway.Application.Requests.UserRequests;
using LibraryGateway.Domain.Enums;

namespace LibraryGateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _config;


    public AuthenticationController(IUserService userService, ITokenService tokenService, IConfiguration config)
    {
        _userService = userService;
        _tokenService = tokenService;
        _config = config;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
    {
        var user = await _userService.GetUserByUserNameAsync(userLoginRequest.UserName);
        if (user == null) return NotFound("Usuário e/ou senha inválido/s");

        var result = await _userService.CheckUserPassword(user.Senha, userLoginRequest.Senha);
        if (!result) return NotFound("Usuário e/ou senha inválido/s");

        return Ok(new
        {
            usuario = user,
            perfil = user.Perfil.ToString(),
            token = _tokenService.CreateToken(user).Result
        });
    }
}

