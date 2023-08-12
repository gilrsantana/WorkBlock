using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;

namespace WorkBlockApp.Pages.Employees;

public class Create : PageModel
{
    [BindProperty]
    public EmployeeModel EmployeeModel { get; set; } = null!;
    public string HashTransaction = "";
    private readonly IEmployeeService _employeeService;

    public Create(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        try
        {
            var result = await _employeeService.AddEmployeeAsync(EmployeeModel);
            if (result.DadosRetorno != null)
            {
                HashTransaction = result.DadosRetorno.HashTransaction;
                TempData["success"] = $"Empregador criado com sucesso. {HashTransaction}";
                Thread.Sleep(6000);
                return RedirectToPage("/Employees/Index");
            }
            if (result.ErroRetorno is not null)
                TempData["Error"] = $"Erro na criação do colaborador. {result.ErroRetorno[0]}";
            return Page();
        }
        catch (System.Exception)
        {
            TempData["Error"] = $"Erro na criação do colaborador.";
            return Page();
        }
    }
}
