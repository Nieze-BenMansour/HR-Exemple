using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjet.ProjectManagement.Domain.Entites;
using System.Reflection.Emit;

namespace MiniProjet.ProjectManagement.Infrastructure.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(builder => builder.LastName)
            .IsRequired()
            .HasMaxLength(150);
    }
}
