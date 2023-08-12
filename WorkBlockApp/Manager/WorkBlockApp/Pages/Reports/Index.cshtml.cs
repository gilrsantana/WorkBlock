using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkBlockApi.ViewModels;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;

namespace WorkBlockApp.Pages.Reports;

public class Index : PageModel
{
    [BindProperty]
    public ReportModel ReportModel { get; set; } = null!;
    public EmployeeDateViewModel EmployeeDateViewModel { get; set; }
    private readonly IPontoBlockReportsService _pontoBlockReportsService;

    public Index(IPontoBlockReportsService pontoBlockReportsService)
    {
        _pontoBlockReportsService = pontoBlockReportsService;
        EmployeeDateViewModel = new EmployeeDateViewModel();
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            var response = await _pontoBlockReportsService.GetData(ReportModel);
            if (response == null)
            {
                TempData["Error"] = $"Erro interno ao buscar os dados de relatório.";
                return Page();
            }
            else if (response.CodigoHttp.ToString() != "OK")
            {
                if (response.ErroRetorno != null)
                {
                    TempData["Error"] = $"Erro interno. {response.ErroRetorno[0]}";
                    return Page();
                }
                TempData["Error"] = $"Erro interno.";
                return Page();
            }
            if (response.DadosRetorno != null)
            {
                EmployeeDateViewModel = response.DadosRetorno;
                TempData["success"] = $"Relatório gerado com sucesso.";
            }
            
            return Page();
        }
        catch (System.Exception)
        {
            TempData["Error"] = $"Erro na geração do relatório.";
            return Page();
        }
    }
}
