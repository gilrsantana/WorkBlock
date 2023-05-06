using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBlockApi.Model;

namespace WorkBlockApi.Data.Mappings;

public class ContractMap : IEntityTypeConfiguration<ContractModel>
{
    public void Configure(EntityTypeBuilder<ContractModel> builder)
    {
        builder.ToTable("Contracts");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("id")
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(x => x.AddressContract)
            .HasColumnName("addressContract")
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(x => x.Abi)
            .HasColumnName("abi")
            .HasColumnType("text");

        builder.Property(x => x.Bytecode)
            .HasColumnName("bytecode")
            .HasColumnType("text");

        builder.Property("createdAt")
            .HasColumnName("createdAt")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Now.ToUniversalTime());

        builder.HasIndex(x => x.Name, "IDX_CONTRACTS_NAME").IsUnique();
    }
}