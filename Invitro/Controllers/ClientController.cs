using Invitro.Dto;
using Invitro.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Invitro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : Controller
{
    
    private readonly IProcedureHandler _procedureHandler;
    private readonly IAppointmentHandler _appointmentHandler;

    public ClientController(IProcedureHandler procedureHandler, IAppointmentHandler appointmentHandler)
    {
        _procedureHandler = procedureHandler;
        _appointmentHandler = appointmentHandler;
    }

    [AllowAnonymous]
    [HttpGet("get-non-archived-procedures")]
    public async Task<IActionResult> GetNonArchivedProcedures()
    {
        return Ok(await _procedureHandler.GetNonArchivedProcedures());
    }
    
    [HttpPost("create-appointment")]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointment)
    {
        await _appointmentHandler.CreateAppointment(appointment);
        return Ok();
    }

    [HttpGet("get-reserved-dates")]
    public async Task<IActionResult> GetReservedDates()
    {
        return Ok(await _appointmentHandler.GetReservedDates());
    }
    
}