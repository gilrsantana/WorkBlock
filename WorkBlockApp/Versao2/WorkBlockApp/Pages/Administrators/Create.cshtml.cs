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
                return RedirectToPage("/Administrators/Index");
            }
            TempData["error"] = "Erro na criação do administrador";
            return RedirectToPage("/Administrators/Index");
        }
        catch (Exception)
        {
            return Page();
        }
    }
}