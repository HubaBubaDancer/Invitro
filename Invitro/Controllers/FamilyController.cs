using Invitro.Dto;
using Invitro.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Invitro.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FamilyController : Controller
{
    
    private readonly IFamilyHandler _familyHandler; 
    
    public FamilyController(IFamilyHandler familyHandler)
    {
        _familyHandler = familyHandler;
    }
    
    [HttpPost("create-family")]
    public async Task<IActionResult> CreateChild([FromBody] ChildDto childDto)
    {
        await _familyHandler.CreateChild(childDto);
        return Ok();
    }
    
    [HttpDelete("delete-child/{id}")]
    public async Task<IActionResult> DeleteChild(Guid id)
    {
        await _familyHandler.DeleteChild(id);
        return Ok();
    }
    
    [HttpPut("update-child/{id}")]
    public async Task<IActionResult> UpdateChild([FromBody] ChildDto childDto, Guid id)
    {
        await _familyHandler.UpdateChild(childDto, id);
        return Ok();
    }
    
    [HttpGet("get-children-by-parent-id/{parentId}")]
    public async Task<IActionResult> GetChildrenByParentId(Guid parentId)
    {
        return Ok(await _familyHandler.GetChildrenByParentId(parentId));
    }
    
    [HttpGet("get-analyses-by-child-id/{childId}")]
    public async Task<IActionResult> GetAnalysesByChildId(Guid childId)
    {
        return Ok(await _familyHandler.GetAnalysesByChildId(childId));
    }
    
    
    
    
}