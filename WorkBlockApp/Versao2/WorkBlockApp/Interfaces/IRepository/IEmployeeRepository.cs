using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;
using WorkBlockApp.ViewModels.VMEmployee;

namespace WorkBlockApp.Interfaces.IRepository;

public interface IEmployeeRepository
{
    Task<ResponseGenerico<IEnumerable<EmployeeIndexViewModel>>> GetEmployeesAsync();
    Task<ResponseGenerico<EmployeeResponse>> GetEmployeeAsync(int id);
    Task<ResponseGenerico<EmployeeResponse>> GetEmployeeByAddressAsync(string address);
    Task<ResponseGenerico<EmployeeAddedEventModel>> AddEmployeeAsync(EmployeeModel employer);
    Task<ResponseGenerico<EmployeeModel>> UpdateEmployeeAsync(EmployeeUpdateModel employer);
    Task<bool> CheckIfEmployeeExistsAsync(string address);
}
