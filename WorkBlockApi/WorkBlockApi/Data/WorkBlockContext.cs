using Microsoft.EntityFrameworkCore;
using WorkBlockApi.Model;

namespace WorkBlockApi.Data;

public class WorkBlockContext : DbContext
{
    public DbSet<ContractModel> Contracts { get; set; }

    public WorkBlockContext(DbContextOptions<WorkBlockContext> options)
    : base(options)
    { }
}