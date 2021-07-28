using Domain.Companies;
using Domain.Companies.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class CompanyTypeConfiguration : BasicEntityTypeConfiguration<Company>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies");

            builder.Property(p => p.CompanyName)
                .HasConversion(p => p.Value, p => CompanyName.Create(p).Value)
                .HasColumnName("CompanyName")
                .IsRequired();
        }
    }
}