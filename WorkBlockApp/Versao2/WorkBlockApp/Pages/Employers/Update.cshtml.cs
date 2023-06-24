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

namespace WorkBlockApp.Pages.Employers
{
    public class Update : PageModel
    {
        [BindProperty]
        public EmployerUpdateModel EmployerUpdateModel { get; set; } = null!;
        public string HashTransaction = "";
        public string? Carteira = "";

        private readonly IEmployerService _employerService;

        public Update(IEmployerService employerService)
        {
            _employerService = employerService;
        }

        public async Task OnGet(string carteira)
        {
            Carteira = HttpContext.Request.Query["carteira"];
            if (!string.IsNullOrEmpty(carteira))
                await SetEmployerUpdateModel(carteira);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                var result = await _employerService.UpdateEmployerAsync(EmployerUpdateModel);
                if (result.DadosRetorno != null)
                {
                    HashTransaction = result.DadosRetorno.HashTransaction;
                    TempData["success"] = $"Empregador atualizado com sucesso. {HashTransaction}";
                    Thread.Sleep(6000);
                    return RedirectToPage("/Employers/Index");
                }
                if (result.ErroRetorno is not null)
                    TempData["Error"] = $"Erro na atualização do empregador. {result.ErroRetorno[0]}";
                return Page();
            }
            catch (Exception)
            {
                TempData["Error"] = $"Erro na atualização do empregador.";
                return Page();
            }
        }

        private async Task SetEmployerUpdateModel(string? address)
        {
            if (string.IsNullOrEmpty(address)) return;
            var employerRequest = await GetEmployerByAddress(address);
            if (employerRequest is { DadosRetorno: not null })
                EmployerUpdateModel = employerRequest.DadosRetorno;
            else
            {
                TempData["error"] = "Erro na atualização do empregador";
                RedirectToPage("/Employers/Index");
            }
        }

        public async Task<ResponseGenerico<EmployerUpdateModel>?> GetEmployerByAddress(string address)
        {
            var employerRequest = await _employerService.GetEmployerByAddressAsync(address);
            return employerRequest;
        }

    }
}