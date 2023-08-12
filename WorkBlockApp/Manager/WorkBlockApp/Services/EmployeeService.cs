using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IRepository;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;
using WorkBlockApp.ViewModels.VMEmployee;

namespace WorkBlockApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<ResponseGenerico<IEnumerable<EmployeeIndexViewModel>>> GetEmployeesAsync()
        {
            return await _employeeRepository.GetEmployeesAsync(); 
        }

        public Task<ResponseGenerico<EmployeeModel>> GetEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseGenerico<EmployeeUpdateModel>> GetEmployeeByAddressAsync(string address)
        {
            var employee = await _employeeRepository.GetEmployeeByAddressAsync(address);
            var result = employee.DadosRetorno;
            if (result is null) return new ResponseGenerico<EmployeeUpdateModel>
            {
                CodigoHttp = employee.CodigoHttp,
                DadosRetorno = null,
                ErroRetorno = employee.ErroRetorno
            };
            var model = new EmployeeUpdateModel
            {
                Nome = result.Nome,
                Pis = result.Pis,
                Carteira = result.Carteira,
                InicioJornada = new TimeOnly((int)result.InicioJornada/100, (int)result.InicioJornada%100),
                FimJornada = new TimeOnly((int)result.FimJornada/100, (int)result.FimJornada%100),
                Empregador = result.Empregador,
                Ativo = result.Ativo
            };
            return new ResponseGenerico<EmployeeUpdateModel>
            {
                CodigoHttp = employee.CodigoHttp,
                DadosRetorno = model,
                ErroRetorno = employee.ErroRetorno
            };
        }

        public async Task<ResponseGenerico<EmployeeAddedEventModel>> AddEmployeeAsync(EmployeeModel employee)
        {
            return await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task<ResponseGenerico<EmployeeUpdateViewModel>> UpdateEmployeeAsync(EmployeeUpdateModel employee)
        {
            return await _employeeRepository.UpdateEmployeeAsync(employee);
        }

        public Task<bool> CheckIfEmployeeExistsAsync(string address)
        {
            throw new NotImplementedException();
        }

    }
}