using MiniProjet.ProjectManagement.Domain.Entites;

namespace MiniProjet.ProjectManagement.Services.Services.Departements;

public interface IDepartementService
{
    Task<int> AddAsync(string name, string description, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Departement>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Departement?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Departement departement, CancellationToken cancellationToken = default);
}