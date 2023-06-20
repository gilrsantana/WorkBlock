using System.Net.Sockets;
using AutoMapper;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IRepository;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;
using WorkBlockApp.Models.ValueObjects;

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
    
    public async Task<ResponseGenerico<EmployerResponseUpdate>> GetEmployerAsync(int id)
    {
        var employer = await _employerRepository.GetEmployerAsync(id);
        return _mapper.Map<ResponseGenerico<EmployerResponseUpdate>>(employer);
    }

    public async Task<ResponseGenerico<EmployerUpdateModel>> GetEmployerByAddressAsync(string address)
    {
        var employer = await _employerRepository.GetEmployerByAddressAsync(address);
        var result = employer.DadosRetorno;
        if (result is null) return new ResponseGenerico<EmployerUpdateModel>
        {
            CodigoHttp = employer.CodigoHttp,
            DadosRetorno = null,
            ErroRetorno = employer.ErroRetorno
        };

        var endereco = new AddressModel(result.Endereco);
        var model = new EmployerUpdateModel
        {
            Nome = result.Nome,
            Cnpj = result.Cnpj,
            Carteira = result.Carteira,
            Endereco = endereco
        };

        return new ResponseGenerico<EmployerUpdateModel>
        {
            CodigoHttp = employer.CodigoHttp,
            DadosRetorno = model,
            ErroRetorno = employer.ErroRetorno
        };
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
