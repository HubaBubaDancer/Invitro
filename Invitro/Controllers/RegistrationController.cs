using Invitro.Dto;
using Invitro.Models;
using Invitro.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Invitro.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class RegistrationController : Controller
{
    private readonly IRegistrationHandler _registration;

    public RegistrationController(IRegistrationHandler registrationHandler)
    {
        _registration = registrationHandler;
    }

    
    [Authorize(Roles = "Doctor,Admin")]
    [HttpPost("/register-doctor")]
    public async Task<IActionResult> RegisterUser([FromBody] DoctorDto doctor)
    {
        await _registration.RegisterDoctor(doctor);
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost("/register-user")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            await _registration.RegisterUser(model);
        }
        
        return Ok();
    } 
}