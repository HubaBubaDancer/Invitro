namespace Invitro.Dto;

public class AppointmentDto
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ProcedureId { get; set; }
    public DateTime Date { get; set; }
    public Guid DepartmentId { get; set; }
}