using MiniProjet.ProjectManagement.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace MiniProjet.ProjectManagement.Domain.Entites;

public class Departement : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Employee> Employees { get; set; }
}