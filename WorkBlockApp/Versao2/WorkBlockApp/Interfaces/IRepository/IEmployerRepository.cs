using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;
using WorkBlockApp.DTOs;
using WorkBlockApp.ViewModels.VMEmployer;

namespace WorkBlockApp.Interfaces.IRepository;

public interface IEmployerRepository
{
    Task<ResponseGenerico<IEnumerable<EmployerIndexViewModel>>> GetEmployersAsync();
    Task<ResponseGenerico<EmployerModel>> GetEmployerAsync(int id);
    Task<ResponseGenerico<EmployerModel>> GetEmployerByAddressAsync(string address);
    Task<ResponseGenerico<EmployerAddedEventModel>> AddEmployerAsync(EmployerModel employer);
    Task<ResponseGenerico<EmployerUpdateViewModel>> UpdateEmployerAsync(EmployerUpdateModel employer);
    Task<bool> CheckIfEmployerExistsAsync(string address);
}
