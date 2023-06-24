using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;

namespace WorkBlockApp.Pages.Employees
{
    public class Update : PageModel
    {
        [BindProperty]
        public EmployeeUpdateModel EmployeeUpdateModel { get; set; } = null!;
        public string HashTransaction = "";
        private readonly IEmployeeService _employeeService;

        public Update(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task OnGet(string carteira)
        {
            if (!string.IsNullOrEmpty(carteira))
                await SetEmployeeUpdateModel(carteira);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(EmployeeUpdateModel);
                if (result.DadosRetorno != null)
                {
                    HashTransaction = result.DadosRetorno.HashTransaction;
                    TempData["success"] = $"Colaborador atualizado com sucesso. {HashTransaction}";
                    Thread.Sleep(6000);
                    return RedirectToPage("/Employees/Index");
                }
                if (result.ErroRetorno is not null)
                    TempData["Error"] = $"Erro na atualização do colaborador. {result.ErroRetorno[0]}";
                return Page();
            }
            catch (Exception)
            {
                TempData["Error"] = $"Erro na atualização do colaborador.";
                return Page();
            }
        }

        private async Task SetEmployeeUpdateModel(string carteira)
        {
            var employeeRequest = await _employeeService.GetEmployeeByAddressAsync(carteira);
            if (employeeRequest.DadosRetorno == null) 
                EmployeeUpdateModel = new EmployeeUpdateModel();
            else
                EmployeeUpdateModel = new EmployeeUpdateModel
                {
                    Carteira = employeeRequest.DadosRetorno.Carteira,
                    Empregador = employeeRequest.DadosRetorno.Empregador,
                    Pis = employeeRequest.DadosRetorno.Pis,
                    Nome = employeeRequest.DadosRetorno.Nome,
                    InicioJornada = employeeRequest.DadosRetorno.InicioJornada,
                    FimJornada = employeeRequest.DadosRetorno.FimJornada,
                    Ativo = employeeRequest.DadosRetorno.Ativo
                };
        }
    }
}