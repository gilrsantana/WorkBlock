using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;

namespace WorkBlockApp.Services;

public class EmployerService : IEmployerService
{
    public Task<ResponseGenerico<IEnumerable<EmployerResponse>>> GetEmployersAsync()
    {
        throw new NotImplementedException();
    }
    
    public Task<ResponseGenerico<EmployerResponse>> GetEmployerAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseGenerico<EmployerUpdateModel>?> GetEmployerByAddressAsync(string address)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseGenerico<EmployerAddedEventModel>> AddEmployerAsync(EmployerModel employer)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseGenerico<EmployerUpdateViewModel>> UpdateEmployerAsync(EmployerUpdateModel employer)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckIfEmployerExistsAsync(string address)
    {
        throw new NotImplementedException();
    }
}
