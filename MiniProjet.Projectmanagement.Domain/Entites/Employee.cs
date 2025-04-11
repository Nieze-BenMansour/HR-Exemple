using MiniProjet.ProjectManagement.Domain.Abstractions;

namespace MiniProjet.ProjectManagement.Domain.Entites;

public class Employee : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int DepartementId { get; set; }
    public Departement Departement { get; set; }
}
