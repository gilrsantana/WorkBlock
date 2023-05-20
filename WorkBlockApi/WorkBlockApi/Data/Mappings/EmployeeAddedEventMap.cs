using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBlockApi.Models.Employee.Events;

namespace WorkBlockApi.Data.Mappings;

public class EmployeeAddedEventMap : IEntityTypeConfiguration<EmployeeAddedEventModel>
{
    public void Configure(EntityTypeBuilder<EmployeeAddedEventModel> builder)
    {
        builder.ToTable("EmployeeAddedEvent");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("VARCHAR")
            .HasMaxLength(36)
            .HasDefaultValue(Guid.NewGuid());

        builder.Property(e => e.AddressFrom)
            .IsRequired()
            .HasColumnName("AddressFrom")
            .HasColumnType("VARCHAR")
            .HasMaxLength(45);

        builder.Property(e => e.EmployeeAddress)
            .IsRequired()
            .HasColumnName("EmployeeAddress")
            .HasColumnType("VARCHAR")
            .HasMaxLength(45);

        builder.Property(e => e.EmployeeName)
            .IsRequired()
            .HasColumnName("EmployeeName")
            .HasColumnType("VARCHAR")
            .HasMaxLength(90);

        builder.Property(e => e.EmployeeTaxId)
            .IsRequired()
            .HasColumnName("EmployeeTaxId")
            .HasColumnType("VARCHAR")
            .HasMaxLength(24);

        builder.Property(e => e.EmployeeBegginingWorkDay)
            .IsRequired()
            .HasColumnName("EmployeeBegginingWorkDay")
            .HasColumnType("TIME");

        builder.Property(e => e.EmployeeEndWorkDay)
            .IsRequired()
            .HasColumnName("EmployeeEndWorkDay")
            .HasColumnType("TIME");

        builder.Property(e => e.EmployerAddress)
            .IsRequired()
            .HasColumnName("EmployerAddress")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(e => e.Time)
            .IsRequired()
            .HasColumnName("Time")
            .HasColumnType("DateTime");

        builder.Property(e => e.HashTransaction)
            .IsRequired()
            .HasColumnName("HashTransaction")
            .HasColumnType("VARCHAR")
            .HasMaxLength(70);
    }
}