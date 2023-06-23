using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;
using WorkBlockApp.ViewModels.VMEmployee;

namespace WorkBlockApp.Interfaces.IServices;

public interface IEmployeeService
{
    Task<ResponseGenerico<IEnumerable<EmployeeIndexViewModel>>> GetEmployeesAsync();
    Task<ResponseGenerico<EmployeeModel>> GetEmployeeAsync(int id);
    Task<ResponseGenerico<EmployeeModel>> GetEmployeeByAddressAsync(string address);
    Task<ResponseGenerico<EmployeeAddedEventModel>> AddEmployeeAsync(EmployeeModel employee);
    Task<ResponseGenerico<EmployeeModel>> UpdateEmployeeAsync(EmployerUpdateModel employee); 
    Task<bool> CheckIfEmployeeExistsAsync(string address);
}