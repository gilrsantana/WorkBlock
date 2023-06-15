using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Event;

namespace WorkBlockApp.Pages.AdministratorCRUD;

public class Cadastrar : PageModel
{
    [BindProperty] 
    public AdministratorModel AdministratorModel { get; set; } = null!;

    [BindProperty] 
    public AdministratorUpdateModel AdministratorUpdateModel { get; set; } = null!;
    public AdminAddedEventModel? AdminAddedEventModel { get; set; }
    public bool IsActive { get; set; }
    
    private readonly IAdministratorService _administratorService;
    
    public string? Id = "";
    public string HashTransaction = "";
    public Cadastrar(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    public async Task OnGetAsync()
    {
        Id = HttpContext.Request.Query["id"];
        if(!string.IsNullOrEmpty(Id))
            await SetAdministratorUpdateModel(Id);
        // SetValues
    }
    
    private async Task SetAdministratorUpdateModel(string? address)
    {
        if (string.IsNullOrEmpty(address)) return;
        var administratorRequest = await GetAdministratorByAddress(address);
        if (administratorRequest is { DadosRetorno: not null })
            AdministratorUpdateModel = administratorRequest.DadosRetorno;
        else
        {
            AdministratorUpdateModel = new AdministratorUpdateModel
            {
                Nome = "",
                Cpf = "",
                Carteira = "",
                Ativo = 0
            };
        }
    }
    
    public async void OnPost()
    {
        if (ModelState.IsValid)
        {
            if (Id is not null)
            {
                try
                {
                    AdministratorUpdateModel.Ativo = IsActive ? 1 : 0;
                    var result = await _administratorService.Update(AdministratorUpdateModel);
                    if (result.DadosRetorno != null)
                        HashTransaction = result.DadosRetorno.HashTransaction;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                try
                {
                    var result = await _administratorService.Add(AdministratorModel);
                    if (result.DadosRetorno != null)
                    {
                        AdminAddedEventModel = new AdminAddedEventModel
                        {
                            Id = result.DadosRetorno.Id,
                            AddressFrom = result.DadosRetorno.AddressFrom,
                            AdministratorAddress = result.DadosRetorno.AdministratorAddress,
                            AdministratorName = result.DadosRetorno.AdministratorName,
                            AdministratorTaxId = result.DadosRetorno.AdministratorTaxId,
                            Time = result.DadosRetorno.Time,
                            HashTransaction = result.DadosRetorno.HashTransaction
                        };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            
        }
    }
    
    
    private async void SetValues()
    {
        var administratorRequest = await GetAdministratorById(0);

        if (administratorRequest is null)
            return;
        
        AdministratorModel = new AdministratorModel
        {
            Nome = administratorRequest.DadosRetorno is null ? "" : administratorRequest.DadosRetorno.Nome,
            Cpf = administratorRequest.DadosRetorno is null ? "" : administratorRequest.DadosRetorno.Cpf,
            Carteira = administratorRequest.DadosRetorno is null ? "" : administratorRequest.DadosRetorno.Carteira,
            Ativo = administratorRequest.DadosRetorno?.Ativo ?? 0
        };
    }
    
    public async Task<ResponseGenerico<AdministratorResponse>?> GetAdministratorById(int id)
    {
        return await _administratorService.GetById(id);
    }
    
    public async Task<ResponseGenerico<AdministratorUpdateModel>?> GetAdministratorByAddress(string address)
    {
        return await _administratorService.GetByAddress(address);
    }
}