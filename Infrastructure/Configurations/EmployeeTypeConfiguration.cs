using Domain.Employees;
using Domain.Employees.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class EmployeeTypeConfiguration : BasicEntityTypeConfiguration<Employee>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employees");


            builder.Property(p => p.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(pp => pp.FirstName)
                    .HasColumnName("first_name")
                    .IsRequired();

                name.Property(pp => pp.LastName)
                    .HasColumnName("last_name")
                    .IsRequired();
            });

            builder.OwnsOne(p => p.Address, address =>
            {
                address.Property(pp => pp.Street)
                    .HasColumnName("address1");

                address.Property(pp => pp.City)
                    .HasColumnName("city");

                address.Property(pp => pp.Country)
                    .HasColumnName("country");
            });

            builder.Property(p => p.CompanyId)
                .HasColumnName("company_id")
                .IsRequired();

            builder.HasOne(p => p.Company)
                .WithMany(p => p.Employees)
                .HasForeignKey(fk => fk.CompanyId)
                .IsRequired();
        }
    }
}