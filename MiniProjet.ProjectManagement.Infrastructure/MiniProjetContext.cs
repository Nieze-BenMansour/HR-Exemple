using Microsoft.EntityFrameworkCore;
using MiniProjet.ProjectManagement.Domain.Entites;
using MiniProjet.ProjectManagement.Infrastructure.Configurations;

namespace MiniProjet.ProjectManagement.Infrastructure;

public class MiniProjetContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Departement> Departements { get; set; }

    public MiniProjetContext()
    {
        
    }

    public MiniProjetContext(DbContextOptions options) : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder
    //        .UseSqlServer(
    //        "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MiniProjetDb;Data Source=LAPTOP-UR7S8C4K;Encrypt=False;");
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}

