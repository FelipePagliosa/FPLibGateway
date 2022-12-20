using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryGateway.Application.Interfaces;
using LibraryGateway.Domain.Enums;
using LibraryGateway.Domain.Exceptions;
using LibraryGateway.Application.Requests.LivroRequests;

namespace LibraryGateway.API.Controllers;

[Authorize(Roles = nameof(Perfil.Administrador))]
[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroConfigService _livroService;

    public LivroController(ILivroConfigService livroService)
    {
        _livroService = livroService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var livros =  _livroService.GetAllAsync().Result.Livros;
            return Ok(livros);
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

    [HttpPost]
    public async Task<IActionResult> Post(LivroInsertRequest request)
    {
        try
        {
            await _livroService.Add(request);
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
}

