namespace WorkBlockApi.Models.Employee;

public class EmployeeModel
{
    public uint IdEmployee { get; set; }
    public string Address { get; set; } = null!;
    public string TaxId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public uint BegginingWorkDay { get; set; }
    public uint EndWorkDay { get; set; }
    public int StateOf { get; set; }
    public string EmployerAddress { get; set; } = null!;
}