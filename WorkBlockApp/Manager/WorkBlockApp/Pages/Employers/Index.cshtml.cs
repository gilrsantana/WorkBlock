using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IServices;

namespace WorkBlockApp.Pages.Employers;

public class Index : PageModel
{
    [BindProperty]
    public List<EmployerResponse> Employers { get; set; } = null!;
    private readonly IEmployerService _employerService;

    public Index(IEmployerService employerService)
    {
        _employerService = employerService;
    }

    public void OnGet()
    {
        Employers = Get().Result ?? new List<EmployerResponse>();
    }

    private async Task<List<EmployerResponse>?> Get()
    {
        var result = await _employerService.GetEmployersAsync();
        if (result.DadosRetorno == null) return null;
        return result.DadosRetorno.ToList<EmployerResponse>();
    }
}
