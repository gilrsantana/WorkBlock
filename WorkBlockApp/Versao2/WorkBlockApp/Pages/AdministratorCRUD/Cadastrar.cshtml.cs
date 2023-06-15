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
    public AdminAddedEventModel AdminAddedEventModel { get; set; } = null!;
    
    private readonly IAdministratorService _administratorService;

    public Cadastrar(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    public void OnGet()
    {
        // SetValues();
    }
    
    public async void OnPost()
    {
        if (ModelState.IsValid)
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
    
    
    private async void SetValues()
    {
        var administratorRequest = await GetAdministrator(0);

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
    
    public async Task<ResponseGenerico<AdministratorResponse>?> GetAdministrator(int id)
    {
        return await _administratorService.Get(id);
    }
    
    
}