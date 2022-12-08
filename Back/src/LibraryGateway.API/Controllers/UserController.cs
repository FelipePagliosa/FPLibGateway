using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryGateway.Application.Interfaces;
using LibraryGateway.Application.Requests.UserRequests;
using LibraryGateway.Domain.Enums;
using LibraryGateway.Domain.Exceptions;

namespace LibraryGateway.API.Controllers;

[Authorize(Roles = nameof(Perfil.Medico))]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    [Authorize(Roles = nameof(Perfil.Medico))]
    public async Task<IActionResult> Get()
    {
        return Ok(await _userService.GetAll());
    }

    [HttpPost]
    [Authorize(Roles = nameof(Perfil.Medico))]

    public async Task<IActionResult> Post(UserInsertRequest request)
    {
        try
        {
            await _userService.Add(request);
            return Ok();
        }
        catch (LibraryGatewayExceptions e)
        {
            return new JsonResult(new { message = e.Message }) { StatusCode = StatusCodes.Status400BadRequest };
        }
        catch (Exception e)
        {
            return new JsonResult(new { message = e.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        }

    }

    [HttpPut]
    [Authorize(Roles = nameof(Perfil.Medico))]
    public async Task<IActionResult> Put(UserUpdateRequest request)
    {
        await _userService.Update(request);
        return Ok();
    }

    [HttpPut("changePassword")]
    [Authorize(Roles = nameof(Perfil.Medico))]
    public async Task<IActionResult> ChangePassword(UserChangePasswordRequest request)
    {
        await _userService.ChangePassword(request);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(Perfil.Medico))]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Delete(id);
        return Ok();
    }
}

