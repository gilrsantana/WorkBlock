using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;

namespace WorkBlockApp.Pages.Employers;

public class Create : PageModel
{
    [BindProperty]
    public EmployerModel EmployerModel { get; set; } = null!;
    public string HashTransaction = "";
    private readonly IEmployerService _employerService;

    public Create(IEmployerService employerService)
    {
        _employerService = employerService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        try
        {
            var result = await _employerService.AddEmployerAsync(EmployerModel);
            if (result.DadosRetorno != null)
            {
                HashTransaction = result.DadosRetorno.HashTransaction;
                TempData["success"] = $"Empregador criado com sucesso. {HashTransaction}";
                Thread.Sleep(6000);
                return RedirectToPage("/Employers/Index");
            }
            if (result.ErroRetorno is not null)
                TempData["Error"] = $"Erro na criação do empregador. {result.ErroRetorno[0]}";
            return Page();
        }
        catch (System.Exception)
        {
            TempData["Error"] = $"Erro na criação do empregador.";
            return Page();
        }
    }
}
