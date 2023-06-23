using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.ViewModels.VMEmployee;

namespace WorkBlockApp.Pages.Employees;

public class Index : PageModel
{
    [BindProperty]
    public List<EmployeeIndexViewModel> EmployeesList { get; set; } = null!;
    public string NomeEmpregador { get; set; } = null!;
    private readonly IEmployeeService _employeeService;
    private readonly IEmployerService _employerService;

    public Index(IEmployeeService employeeService, IEmployerService employerService)
    {
        _employeeService = employeeService;
        _employerService = employerService;
    }

    public void OnGet()
    {
        EmployeesList = Get().Result ?? new List<EmployeeIndexViewModel>();
    }

    private async Task<List<EmployeeIndexViewModel>?> Get()
    {
        var result = await _employeeService.GetEmployeesAsync();
        if (result.DadosRetorno == null) return null;
        return result.DadosRetorno.ToList<EmployeeIndexViewModel>();
    }

    public async Task<string> GetNomeEmpregador(string carteiraEmpregador)
    {
        var result = await _employerService.GetEmployerByAddressAsync(carteiraEmpregador);
        if (result.DadosRetorno == null) return "Não encontrado";
        return result.DadosRetorno.Nome;
    }
}