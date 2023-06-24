using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Event;

namespace WorkBlockApp.Pages.Administrators;

public class Create : PageModel
{
    [BindProperty]
    public AdministratorModel AdministratorModel { get; set; } = null!;

    private readonly IAdministratorService _administratorService;

    public string HashTransaction = "";
    public Create(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            var result = await _administratorService.Add(AdministratorModel);
            if (result.DadosRetorno != null)
            {
                HashTransaction = result.DadosRetorno.HashTransaction;
                TempData["success"] = $"Administrador criado com sucesso. {HashTransaction}";
                Thread.Sleep(6000);
                return RedirectToPage("/Administrators/Index");
            }
            if (result.ErroRetorno is not null)
                    TempData["Error"] = $"Erro na criação do administrador. {result.ErroRetorno[0]}";
            return Page();
        }
        catch (Exception)
        {
            TempData["Error"] = $"Erro na criação do administrador.";
            return Page();
        }
    }
}