using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBlockApi.Models.Employer.Events;

namespace WorkBlockApi.Data.Mappings;

public class EmployerUpdatedEventMap : IEntityTypeConfiguration<EmployerUpdatedEventModel>
{
    public void Configure(EntityTypeBuilder<EmployerUpdatedEventModel> builder)
    {
        builder.ToTable("EmployerUpdatedEvent");

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

        builder.Property(e => e.OldAddress)
            .IsRequired()
            .HasColumnName("OldAddress")
            .HasColumnType("VARCHAR")
            .HasMaxLength(45);

        builder.Property(e => e.NewAddress)
            .IsRequired()
            .HasColumnName("NewAddress")
            .HasColumnType("VARCHAR")
            .HasMaxLength(45);

        builder.Property(e => e.EmployerName)
            .IsRequired()
            .HasColumnName("EmployerName")
            .HasColumnType("VARCHAR")
            .HasMaxLength(90);

        builder.Property(e => e.EmployerTaxId)
            .IsRequired()
            .HasColumnName("EmployerTaxId")
            .HasColumnType("VARCHAR")
            .HasMaxLength(24);

        builder.Property(e => e.EmployerLegalAddress)
            .IsRequired()
            .HasColumnName("EmployerLegalAddress")
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