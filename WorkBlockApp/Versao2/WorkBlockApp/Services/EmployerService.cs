using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IRepository;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;

namespace WorkBlockApp.Services;

public class EmployerService : IEmployerService
{
    private readonly IMapper _mapper;
    private readonly IEmployerRepository _employerRepository;

    public EmployerService(IMapper mapper, IEmployerRepository employerRepository)
    {
        _mapper = mapper;
        _employerRepository = employerRepository;
    }

    public async Task<ResponseGenerico<IEnumerable<EmployerResponse>>> GetEmployersAsync()
    {
        var employers = await _employerRepository.GetEmployersAsync();
        return _mapper.Map<ResponseGenerico<IEnumerable<EmployerResponse>>>(employers);
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
