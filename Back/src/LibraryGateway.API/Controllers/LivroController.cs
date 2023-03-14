using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryGateway.Application.Interfaces;
using LibraryGateway.Domain.Enums;
using LibraryGateway.Domain.Exceptions;
using LibraryGateway.Application.Requests.LivroRequests;

namespace LibraryGateway.API.Controllers;


[Authorize(Roles = nameof(Perfil.Administrador) + "," + nameof(Perfil.Usuario))]
[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroConfigService _livroService;
    //authenticated user DI
    

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

    [HttpPost("filter")]
    public async Task<IActionResult> GetByFilter(LivroFilter filter)
    {
        try
        {
            var livros = _livroService.GetByFilterAsync(filter).Result.Livros;
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

    [Authorize(Roles = nameof(Perfil.Administrador))]
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

    [HttpGet("user")]
    public async Task<IActionResult> GetLivrosByUser()
    {
        try
        {
            var livros = _livroService.GetByUserAsync().Result.Livros;
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

    //link a book to a user
    [HttpGet("link/{id}")]
    public async Task<IActionResult> LinkLivroToUser(int id)
    {
        try
        {
            await _livroService.LinkLivroToUser(id);
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

    [Authorize(Roles = nameof(Perfil.Administrador))]
    [HttpPut]
    public async Task<IActionResult> Put(LivroUpdateRequest request)
    {
        try
        {
            await _livroService.Update(request);
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

    [Authorize(Roles = nameof(Perfil.Administrador))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _livroService.Delete(id);
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

