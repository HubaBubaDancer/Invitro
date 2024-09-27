namespace Invitro.Models;

public class RegisterModel
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string? patronymicName { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string password { get; set; }
}