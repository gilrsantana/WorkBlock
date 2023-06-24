using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Event;

namespace WorkBlockApp.Pages.Administrators;

public class Update : PageModel
{
    [BindProperty]
    public AdministratorUpdateModel AdministratorUpdateModel { get; set; } = null!;
    public AdminAddedEventModel? AdminAddedEventModel { get; set; }

    private readonly IAdministratorService _administratorService;

    public string? Carteira = "";
    public string HashTransaction = "";
    public Update(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }
    public async Task OnGetAsync(string carteira)
    {
        Carteira = HttpContext.Request.Query["carteira"];
        if (!string.IsNullOrEmpty(carteira))
            await SetAdministratorUpdateModel(carteira);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            var result = await _administratorService.Update(AdministratorUpdateModel);
            if (result.DadosRetorno != null)
            {
                HashTransaction = result.DadosRetorno.HashTransaction;
                TempData["success"] = $"Administrador atualizado com sucesso. {HashTransaction}";
                Thread.Sleep(6000);
                return RedirectToPage("/Administrators/Index");
            }
            if (result.ErroRetorno is not null)
                TempData["Error"] = $"Erro na atualização do administrador. {result.ErroRetorno[0]}";
            return Page();
        }
        catch (Exception e)
        {
            TempData["Error"] = $"Erro na atualização do administrador.";
            Console.WriteLine(e);
            throw;
        }
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

    public async Task<ResponseGenerico<AdministratorUpdateModel>?> GetAdministratorByAddress(string address)
    {
        return await _administratorService.GetByAddress(address);
    }
}