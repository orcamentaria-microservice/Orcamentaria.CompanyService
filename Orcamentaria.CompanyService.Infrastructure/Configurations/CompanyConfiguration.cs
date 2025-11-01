using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orcamentaria.CompanyService.Domain.Models;

namespace Orcamentaria.CompanyService.Infrastructure.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("T_COMPANY");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id)
                .HasColumnName("ID")
                .HasColumnType("BIGINT")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(p => p.CorporateName)
                .HasColumnName("CORPORATE_NAME")
                .HasColumnType("VARCHAR(90)")
                .IsRequired();

            builder.Property(p => p.TradeName)
                .HasColumnName("TRADE_NAME")
                .HasColumnType("VARCHAR(120)")
                .IsRequired();

            builder.Property(p => p.Cnpj)
                .HasColumnName("CNPJ")
                .HasColumnType("CHAR(14)")
                .IsRequired();

            builder.Property(p => p.Address)
                .HasColumnName("ADDRESS")
                .HasColumnType("VARCHAR(150)");

            builder.Property(p => p.TelePhone)
                .HasColumnName("TELEPHONE")
                .HasColumnType("VARCHAR(25)");	

            builder.Property(p => p.CellPhone)
                .HasColumnName("CELLPHONE")
                .HasColumnType("VARCHAR(25)");	

            builder.Property(p => p.Email)
                .HasColumnName("EMAIL")
                .HasColumnType("VARCHAR(100)");

            builder.Property(p => p.Active)
                .HasColumnName("ACTIVE")
                .HasColumnType("BIT")
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
