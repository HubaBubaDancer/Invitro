using Invitro.Dto;
using Invitro.Models;
using Microsoft.EntityFrameworkCore;

namespace Invitro.Services;

public interface IFamilyHandler
{
    public Task CreateChild(ChildDto childDto);
    public Task DeleteChild(Guid id);
    public Task UpdateChild(ChildDto childDto, Guid id);
    public Task<List<Child>> GetChildrenByParentId(Guid parentId);
    public Task<List<Analysis>> GetAnalysesByChildId(Guid childId);
}

public class FamilyHandler : IFamilyHandler
{
    private readonly ApplicationDbContext _context;

    public FamilyHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateChild(ChildDto childDto) 
    {
        var child = new Child
        {
            FirstName = childDto.FirstName,
            LastName = childDto.LastName,
            PatronymicName = childDto.PatronymicName,
            gender = childDto.gender,
            BirthDate = childDto.BirthDate,
            ParentId = childDto.ParentId
        };        
        
        await _context.Children.AddAsync(child);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteChild(Guid id)
    {
        var child = _context.Children.FirstOrDefault(c => c.Id == id);
        
        if (child == null)
        {
            return;
        }
        
        _context.Children.Remove(child);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateChild(ChildDto childDto, Guid id)
    {
        var child = _context.Children.FirstOrDefault(c => c.Id == id);
        
        if (child == null)
        {
            return;
        }
        
        child.FirstName = childDto.FirstName;
        child.LastName = childDto.LastName;
        child.PatronymicName = childDto.PatronymicName;
        child.gender = childDto.gender;
        child.BirthDate = childDto.BirthDate;
        child.ParentId = childDto.ParentId;
        
        _context.Children.Update(child);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Child>> GetChildrenByParentId(Guid parentId)
    {
        var children = await _context.Children
            .Where(c => c.ParentId == parentId).ToListAsync();
        return children;
    }

    public async Task<List<Analysis>> GetAnalysesByChildId(Guid childId)
    {   
        var analyses = await _context.Analyses
            .Where(a => a.PatientId == childId).ToListAsync();
        return analyses;
    }
    
    
}
