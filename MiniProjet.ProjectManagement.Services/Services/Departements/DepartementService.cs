using Microsoft.EntityFrameworkCore;
using MiniProjet.ProjectManagement.Domain.Entites;
using MiniProjet.ProjectManagement.Infrastructure;

namespace MiniProjet.ProjectManagement.Services.Services.Departements;

public class DepartementService : IDepartementService
{
    private readonly MiniProjetContext _context;

    public DepartementService(MiniProjetContext context)
    {
        _context = context;
    }

    // pour éviter le DRY (Don't repeat your self) il faut enlever "Departement" du nom de la méthode
    // par ce qu'il existe dans le nom de la classe
    // cancellationToken est paramétre optionnel (suite à une initialisation dans la siganture de la méthode)
    public async Task<int> AddAsync(string name, string description, CancellationToken cancellationToken = default)
    {
        var departement = new Departement
        {
            Name = name,
            Description = description
        };

        _context.Departements.Add(departement); // Charger le modif en mémoire
        await _context.SaveChangesAsync(cancellationToken); // Commit BD // attentre la fin de la tâche car le EF en Thread Safe

        return departement.Id; // Retourne l'id du departement
    }

    public async Task<List<Departement>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departements.ToListAsync(cancellationToken);
    }

    public async Task<Departement?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Departements.FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(Departement departement, CancellationToken cancellationToken = default)
    {
        _context.Departements.Update(departement);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var departement = await GetByIdAsync(id, cancellationToken);
        if (departement != null)
        {
            _context.Departements.Remove(departement);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
