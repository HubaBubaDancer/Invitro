namespace Invitro.Dto;

public class ChildDto
{
    public Guid ParentId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public DateTime BirthDate { get; set; }
    public string gender { get; set; }
}