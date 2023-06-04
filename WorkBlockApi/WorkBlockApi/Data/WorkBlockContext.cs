using Microsoft.EntityFrameworkCore;
using WorkBlockApi.Models;
using WorkBlockApi.Models.Administrator.Events;
using WorkBlockApi.Models.Employee.Events;
using WorkBlockApi.Models.Employer.Events;

namespace WorkBlockApi.Data;

public class WorkBlockContext : DbContext
{
    public DbSet<ContractModel> Contracts { get; set; } = null!;
    public DbSet<AdminAddedEventModel>  AdminAddedEvents { get; set; } = null!;
    public DbSet<AdminUpdatedEventModel> AdminUpdatedEvents { get; set; } = null!;
    public DbSet<EmployerAddedEventModel> EmployerAddedEvents { get; set; } = null!;
    public DbSet<EmployerUpdatedEventModel> EmployerUpdatedEvents { get; set; } = null!;
    public DbSet<EmployeeAddedEventModel> EmployeeAddedEvents { get; set; } = null!;
    public DbSet<EmployeeUpdatedEventModel> EmployeeUpdatedEvents { get; set; } = null!;

    public WorkBlockContext(DbContextOptions<WorkBlockContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContractModel>().ToTable(nameof(Contracts), t => t.ExcludeFromMigrations());
    }
}