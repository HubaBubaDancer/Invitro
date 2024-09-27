using Invitro.Dto;
using Invitro.Models;
using Microsoft.EntityFrameworkCore;

namespace Invitro.Services;

public interface IProcedureHandler
{
    public Task CreateProcedure(ProcedureDto procedure);
    public Task UpdateProcedure(ProcedureDto procedure, Guid id);
    public Task<List<Procedure>> GetProcedures();
    public Task<List<ProcedureDto>> GetNonArchivedProcedures();
}

public class ProceduresHandler : IProcedureHandler
{

    private readonly ApplicationDbContext _context;

    public ProceduresHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateProcedure(ProcedureDto procedureDto)
    {
        var procedure = new Procedure
        {
            name = procedureDto.name,
            price = procedureDto.price,
            description = procedureDto.description,
            archived = procedureDto.archived
        };

        await _context.Procedures.AddAsync(procedure);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProcedure(ProcedureDto procedureDto, Guid id)
    {
        var procedure = _context.Procedures.FirstOrDefault(p => p.Id == id);
        
        procedure.description = procedureDto.description;
        procedure.name = procedureDto.name;
        procedure.price = procedureDto.price;
        procedure.archived = procedureDto.archived;
        
        _context.Procedures.Update(procedure);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Procedure>> GetProcedures()
    {
        return await _context.Procedures.ToListAsync();
    }
    
    public async Task<List<ProcedureDto>> GetNonArchivedProcedures()
    {
        var ans = await _context.Procedures.Where(p => p.archived == false).ToListAsync();
        return ans.Select(p => new ProcedureDto
        {
            name = p.name,
            description = p.description,
            price = p.price,
            archived = p.archived
        }).ToList();
    }
}