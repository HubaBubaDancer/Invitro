using Invitro.Models;

namespace Invitro.Services;

public interface IAIHandler
{
    public Task<string> GetPeriodSummary(DateTime start, DateTime end);
    
}

public class AIHandler : IAIHandler
{
    private readonly ApplicationDbContext _context;

    public AIHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> GetPeriodSummary(DateTime start, DateTime end)
    {
        var appointments =  _context.Appointments
            .Where(x => x.CreatedAt > start && x.CreatedAt < end).ToString();

        var chat = new AmChat();
        var answer = chat.GetAnswer(appointments).Result;

        return answer;
    }
    
    
}