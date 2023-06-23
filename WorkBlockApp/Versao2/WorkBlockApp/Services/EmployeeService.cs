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

        public Task<ResponseGenerico<EmployeeModel>> GetEmployeeByAddressAsync(string address)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseGenerico<EmployeeAddedEventModel>> AddEmployeeAsync(EmployeeModel employee)
        {
            return await _employeeRepository.AddEmployeeAsync(employee);
        }

        public Task<ResponseGenerico<EmployeeModel>> UpdateEmployeeAsync(EmployerUpdateModel employee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckIfEmployeeExistsAsync(string address)
        {
            throw new NotImplementedException();
        }

    }
}