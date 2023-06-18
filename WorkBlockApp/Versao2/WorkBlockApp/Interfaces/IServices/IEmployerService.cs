using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;

namespace WorkBlockApp.Interfaces.IServices;

public interface IEmployerService
{
    Task<ResponseGenerico<IEnumerable<EmployerResponse>>> GetEmployersAsync();
    Task<ResponseGenerico<EmployerResponse>> GetEmployerAsync(int id);
    Task<ResponseGenerico<EmployerUpdateModel>?> GetEmployerByAddressAsync(string address);
    Task<ResponseGenerico<EmployerAddedEventModel>> AddEmployerAsync(EmployerModel employer);
    Task<ResponseGenerico<EmployerUpdateViewModel>> UpdateEmployerAsync(EmployerUpdateModel employer); 
    Task<bool> CheckIfEmployerExistsAsync(string address);

}