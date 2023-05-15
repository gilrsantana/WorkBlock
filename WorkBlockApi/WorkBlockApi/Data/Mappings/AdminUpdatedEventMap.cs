using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBlockApi.Models.Administrator.Events;

namespace WorkBlockApi.Data.Mappings;

public class AdminUpdatedEventMap : IEntityTypeConfiguration<AdminUpdatedEventModel>
{
    public void Configure(EntityTypeBuilder<AdminUpdatedEventModel> builder)
    {
        builder.ToTable("AdminUpdatedEvent");

        builder.HasKey(e => e.Id);

        builder.Property(e=> e.Id)
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

        builder.Property(e => e.State)
            .IsRequired()
            .HasColumnName("State")
            .HasColumnType("TINYINT");

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