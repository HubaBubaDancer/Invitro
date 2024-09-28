using Invitro.Dto;
using Invitro.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Invitro.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminController : Controller
{
    
    private readonly IDepartmentHandler _departmentHandler;
    
    public AdminController(IDepartmentHandler departmentHandler)
    {
        _departmentHandler = departmentHandler;
    }
    
    
    [HttpPost("/create-department")]
    public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto department)
    {
         await _departmentHandler.CreateDepartment(department);
         return Ok();
    }
    
    [HttpPut("/update-department")]
    public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentDto department, Guid id)
    {
         await _departmentHandler.UpdateDepartment(department, id);
         return Ok();
    }
    
    
    [HttpDelete("/delete-department")]
    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
        await _departmentHandler.DeleteDepartment(id);
        return Ok();
    }
    
    
    
    
}