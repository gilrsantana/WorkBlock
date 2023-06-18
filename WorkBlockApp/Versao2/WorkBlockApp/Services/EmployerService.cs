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
    
    public async Task<ResponseGenerico<EmployerResponse>> GetEmployerAsync(int id)
    {
        var employer = await _employerRepository.GetEmployerAsync(id);
        return _mapper.Map<ResponseGenerico<EmployerResponse>>(employer);
    }

    public async Task<ResponseGenerico<EmployerResponse>> GetEmployerByAddressAsync(string address)
    {
        var employer = await _employerRepository.GetEmployerByAddressAsync(address);
        return _mapper.Map<ResponseGenerico<EmployerResponse>>(employer);
    }

    public async Task<ResponseGenerico<EmployerAddedEventModel>> AddEmployerAsync(EmployerModel employer)
    {
        return await _employerRepository.AddEmployerAsync(employer);
    }

    public async Task<ResponseGenerico<EmployerUpdateViewModel>> UpdateEmployerAsync(EmployerUpdateModel employer)
    {
        return await _employerRepository.UpdateEmployerAsync(employer);
    }

    public Task<bool> CheckIfEmployerExistsAsync(string address)
    {
        return _employerRepository.CheckIfEmployerExistsAsync(address);
    }
}
