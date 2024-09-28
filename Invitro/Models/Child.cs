namespace Invitro.Models;

public class Child : BaseDbItem
{
    public Guid ParentId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string gender { get; set; }
}