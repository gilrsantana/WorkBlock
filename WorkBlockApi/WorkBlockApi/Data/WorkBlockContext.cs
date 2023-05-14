using Microsoft.EntityFrameworkCore;
using WorkBlockApi.Models;
using WorkBlockApi.Models.Administrator.Events;

namespace WorkBlockApi.Data;

public class WorkBlockContext : DbContext
{
    public DbSet<ContractModel> Contracts { get; set; }
    public DbSet<AdminAddedEventModel>  AdminAddedEvents { get; set; }

    public WorkBlockContext(DbContextOptions<WorkBlockContext> options)
    : base(options)
    { }
}