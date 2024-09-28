using System.ComponentModel.Design;
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
    private readonly IDepartmentHandler _departmentHandler;

    public ClientController(IProcedureHandler procedureHandler, IAppointmentHandler appointmentHandler, IDepartmentHandler departmentHandler)
    {
        _procedureHandler = procedureHandler;
        _appointmentHandler = appointmentHandler;
        _departmentHandler = departmentHandler;
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
    
    [HttpGet("get-departments")]
    public async Task<IActionResult> GetDepartments()
    {
        return Ok(await _departmentHandler.GetDepartments());
    }
    
}