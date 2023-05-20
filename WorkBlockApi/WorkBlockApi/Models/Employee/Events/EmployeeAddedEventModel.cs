using Org.BouncyCastle.Asn1.Cms;

namespace WorkBlockApi.Models.Employee.Events;

public class EmployeeAddedEventModel
{
    public Guid Id { get; set; }
    public string AddressFrom { get; set; } = null!;
    public string EmployeeAddress { get; set; } = null!;
    public string EmployeeName { get; set; } = null!;
    public string EmployeeTaxId { get; set; } = null!;
    public TimeOnly EmployeeBegginingWorkDay { get; set; }
    public TimeOnly EmployeeEndWorkDay { get; set; }
    public string EmployerAddress { get; set; } = null!;
    public DateTime Time { get; set; }
    public string HashTransaction { get; set; } = null!;
}