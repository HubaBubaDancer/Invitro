using Invitro.Dto;
using Invitro.Models;
using Microsoft.EntityFrameworkCore;

namespace Invitro.Services;

public interface IDepartmentHandler
{
    public Task CreateDepartment(DepartmentDto department);
    public Task UpdateDepartment(DepartmentDto department, Guid id);
    public Task DeleteDepartment(Guid id);
    public Task<List<Department>> GetDepartments();
}


public class DepartmentHandler : IDepartmentHandler
{
    private readonly ApplicationDbContext _context;

    public DepartmentHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateDepartment(DepartmentDto departmentDto)
    {
        var department = new Department
        {
            Name = departmentDto.Name,
            Street = departmentDto.Street
        };

        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateDepartment(DepartmentDto departmentDto, Guid id)
    {
        var department = _context.Departments.FirstOrDefault(p => p.Id == id);
        
        department.Name = departmentDto.Name;
        department.Street = departmentDto.Street;
        
        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteDepartment(Guid id)
    {
        var department = _context.Departments.FirstOrDefault(p => p.Id == id);
        
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Department>> GetDepartments()
    {
        return await _context.Departments.ToListAsync();
    }
}