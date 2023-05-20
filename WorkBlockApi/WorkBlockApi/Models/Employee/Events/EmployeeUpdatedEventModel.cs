namespace WorkBlockApi.Models.Employee.Events;

public class EmployeeUpdatedEventModel
{
    public Guid Id { get; set; }
    public string AddressFrom { get; set; } = null!;
    public string OldAddress { get; set; } = null!;
    public string NewAddress { get; set; } = null!;
    public string EmployeeName { get; set; } = null!;
    public string EmployeeTaxId { get; set; } = null!;
    public TimeOnly EmployeeBegginingWorkDay { get; set; }
    public TimeOnly EmployeeEndWorkDay { get; set; }
    public string EmployerAddress { get; set; } = null!;
    public byte State { get; set; }
    public DateTime Time { get; set; }
    public string HashTransaction { get; set; } = null!;
}