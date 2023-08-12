using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;

namespace WorkBlockApp.Pages.Administrators;

public class Index : PageModel
{
    [BindProperty] 
    public List<AdministratorResponse> Administrators { get; set; } = null!;
    
    private readonly IAdministratorService _administratorService;

    public Index(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }
    
    public async Task OnGet()
    {
        Administrators = await Get() ?? new List<AdministratorResponse>();
    }
    
    private async Task<List<AdministratorResponse>?> Get()
    {
        var result = await _administratorService.GetAll();
        return result.DadosRetorno ?? null;
    }
}