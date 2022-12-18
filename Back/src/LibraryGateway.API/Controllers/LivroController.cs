using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryGateway.Application.Interfaces;

using LibraryGateway.Domain.Enums;
using LibraryGateway.Domain.Exceptions;

namespace LibraryGateway.API.Controllers;

[Authorize(Roles = nameof(Perfil.Medico))]
[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    
}

