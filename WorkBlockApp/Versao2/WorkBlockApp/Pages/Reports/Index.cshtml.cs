using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;

namespace WorkBlockApp.Pages.Reports;

public class Index : PageModel
{
    [BindProperty]
    public ReportModel ReportModel { get; set; } = null!;
    private readonly IPontoBlockReportsService _pontoBlockReportsService;

    public Index(IPontoBlockReportsService pontoBlockReportsService)
    {
        _pontoBlockReportsService = pontoBlockReportsService;
    }

    public void OnGet()
    {

    }

    public async void OnPost()
    {
        if (!ModelState.IsValid) return;
        try
        {
            var response = await _pontoBlockReportsService.GetWorkTimesFromEmployeeAtDate(ReportModel);
            TempData["success"] = $"Relatório gerado com sucesso.";
            return;
        }
        catch (System.Exception)
        {
            TempData["Error"] = $"Erro na geração do relatório.";
            return;
        }
    }
}
