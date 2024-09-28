namespace Invitro.Models;

public class Analysis : BaseDbItem
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime Date { get; set; }
    public Guid ProcedureId { get; set; }
    
    
}