using Microsoft.EntityFrameworkCore;
using WorkBlockApi.Models;
using WorkBlockApi.Models.Administrator.Events;
using WorkBlockApi.Models.Employer.Events;

namespace WorkBlockApi.Data;

public class WorkBlockContext : DbContext
{
    public DbSet<ContractModel> Contracts { get; set; }
    public DbSet<AdminAddedEventModel>  AdminAddedEvents { get; set; }
    public DbSet<AdminUpdatedEventModel> AdminUpdatedEvents { get; set; }
    public DbSet<EmployerAddedEventModel> EmployerAddedEvents { get; set; }
    public DbSet<EmployerUpdatedEventModel> EmployerUpdatedEvents { get; set; }

    public WorkBlockContext(DbContextOptions<WorkBlockContext> options)
    : base(options)
    { }
}