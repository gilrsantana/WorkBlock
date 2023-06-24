using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;
using WorkBlockApp.ViewModels.VMEmployee;

namespace WorkBlockApp.Interfaces.IServices;

public interface IEmployeeService
{
    Task<ResponseGenerico<IEnumerable<EmployeeIndexViewModel>>> GetEmployeesAsync();
    Task<ResponseGenerico<EmployeeModel>> GetEmployeeAsync(int id);
    Task<ResponseGenerico<EmployeeUpdateModel>> GetEmployeeByAddressAsync(string address);
    Task<ResponseGenerico<EmployeeAddedEventModel>> AddEmployeeAsync(EmployeeModel employee);
    Task<ResponseGenerico<EmployeeUpdateViewModel>> UpdateEmployeeAsync(EmployeeUpdateModel employee); 
    Task<bool> CheckIfEmployeeExistsAsync(string address);
}