using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkBlockApi.Data;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Model;

namespace WorkBlockApi.Repositories;

public class ContractModelRepository : IContractModelRepository
{
    private readonly WorkBlockContext _context;

    public ContractModelRepository([FromServices] WorkBlockContext context)
    {
        _context = context;
    }
    public async Task<ContractModel?> GetByNameAsync(string contractName)
        => await 
            _context
                .Contracts
                .FirstOrDefaultAsync(
                    x => x.Name == contractName);

    public async Task<List<ContractModel>> GetAllContractsAsync()
        => await _context.Contracts.ToListAsync();
}