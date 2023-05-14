using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBlockApi.Models.Administrator.Events;

namespace WorkBlockApi.Data.Mappings;

public class AdminAddedEventMap : IEntityTypeConfiguration<AdminAddedEventModel>
{
    public void Configure(EntityTypeBuilder<AdminAddedEventModel> builder)
    {
        builder.ToTable("AdminAddedEvent");

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

        builder.Property(e => e.AdministratorAddress)
            .IsRequired()
            .HasColumnName("AdministratorAddress")
            .HasColumnType("VARCHAR")
            .HasMaxLength(45);

        builder.Property(e => e.AdministratorName)
            .IsRequired()
            .HasColumnName("AdministratorName")
            .HasColumnType("VARCHAR")
            .HasMaxLength(90);

        builder.Property(e => e.AdministratorTaxId)
            .IsRequired()
            .HasColumnName("AdministratorTaxId")
            .HasColumnType("VARCHAR")
            .HasMaxLength(24);

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