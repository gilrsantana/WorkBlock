using AutoMapper;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IRepository;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Event;

namespace WorkBlockApp.Services;

public class AdministratorService : IAdministratorService
{
    private readonly IMapper _mapper;
    private readonly IAdministratorRepository _administratorRepository;
    
    public AdministratorService(IMapper mapper, IAdministratorRepository administratorRepository)
    {
        _mapper = mapper;
        _administratorRepository = administratorRepository;
    }

    public async Task<ResponseGenerico<AdministratorResponse>> GetById(int id)
    {
        var administrator = await _administratorRepository.GetById(id);
        return _mapper.Map<ResponseGenerico<AdministratorResponse>>(administrator);
    }

    public async Task<ResponseGenerico<AdministratorUpdateModel>?> GetByAddress(string address)
    {
        var administrators = await GetAll();
        var administrator = administrators.DadosRetorno?.FirstOrDefault(x => x.Carteira == address);
        if (administrator is null) return null;
        var result = new AdministratorUpdateModel
        {
            Nome = administrator.Nome ?? "",
            Cpf = administrator.Cpf ?? "",
            Carteira = administrator.Carteira ?? "",
            Ativo = administrator.Ativo
        };
        
        
        return new ResponseGenerico<AdministratorUpdateModel>
        {
            DadosRetorno = result,
            CodigoHttp = administrators.CodigoHttp,
            ErroRetorno = administrators.ErroRetorno
        };
    }

    public async Task<ResponseGenerico<AdminAddedEventModel>> Add(AdministratorModel administrator)
    {
        return await _administratorRepository.Add(administrator);
    }

    public async Task<ResponseGenerico<AdministratorUpdateViewModel>> Update(AdministratorUpdateModel administrator)
    {
        return await _administratorRepository.Update(administrator);
    }

    public async Task<ResponseGenerico<List<AdministratorResponse>>> GetAll()
    {
        var administrators = await _administratorRepository.GetAll();
        return _mapper.Map<ResponseGenerico<List<AdministratorResponse>>>(administrators);
    }
}