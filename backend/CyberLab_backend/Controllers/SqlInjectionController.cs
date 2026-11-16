using Application.Scenarios.SqlInjection;
using Application.Scenarios.SqlInjection.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CyberLab_backend.Controllers;

[ApiController]
[Route("api/sqli")]
public class SqlInjectionController : ControllerBase
{
    private readonly SqlInjectionService _service;
    
    public SqlInjectionController(SqlInjectionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<SqlInjectionResultDto>> SearchSecure([FromQuery] string search = "")
    {
        var result = await _service.SearchSecureAsync(search);
        
        return Ok(result);
    }
}