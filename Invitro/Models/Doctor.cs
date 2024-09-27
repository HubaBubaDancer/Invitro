namespace Invitro.Models;

public class Doctor : BaseDbItem
{
    public Guid UserId { get; set; }
    public int? idFromApi { get; set; }
    public string lastName { get; set; }
    public string firstName { get; set; }
    public string? patronymicName { get; set; }
    public DateTime birthDate { get; set; }
    public string? gender { get; set; }
    public string? barcode { get; set; }
    public string? identificationNumber { get; set; }
    
    public List<Procedure> Procedures { get; set; } = new List<Procedure>();
    
    
}