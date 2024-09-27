namespace Invitro.Models;

public class BaseDbItem
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public BaseDbItem()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}