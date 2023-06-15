using AutoMapper;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IRest;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Event;

namespace WorkBlockApp.Services;

public class AdministratorService : IAdministratorService
{
    private readonly IMapper _mapper;
    private readonly IAdministratorRest _administratorRest;
    
    public AdministratorService(IMapper mapper, IAdministratorRest administratorRest)
    {
        _mapper = mapper;
        _administratorRest = administratorRest;
    }

    public async Task<ResponseGenerico<AdministratorResponse>> GetById(int id)
    {
        var administrator = await _administratorRest.GetById(id);
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
        return await _administratorRest.Add(administrator);
    }

    public async Task<ResponseGenerico<AdministratorUpdateViewModel>> Update(AdministratorUpdateModel administrator)
    {
        return await _administratorRest.Update(administrator);
    }

    public async Task<ResponseGenerico<List<AdministratorResponse>>> GetAll()
    {
        var administrators = await _administratorRest.GetAll();
        return _mapper.Map<ResponseGenerico<List<AdministratorResponse>>>(administrators);
    }
}