using Microsoft.EntityFrameworkCore;
using MiniProjet.ProjectManagement.Domain.Entites;
using MiniProjet.ProjectManagement.Infrastructure;

namespace MiniProjet.ProjectManagement.Services.Services.Departements;

public class EmployeeService
{
    private readonly MiniProjetContext _context;
    public EmployeeService(MiniProjetContext context)
    {
        _context = context;
    }

    public async Task AddAsync(string name, string lastName, int age, int departmentId, CancellationToken cancellationToken = default)
    {
        var employee = new Employee
        {
            Name = name,
            LastName = lastName,
            Age = age,
            DepartementId = departmentId
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Employees.Include(e => e.Departement).ToListAsync(cancellationToken); // Eager loading of Departement
    }

    public async Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Employees.FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await GetByIdAsync(id, cancellationToken);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }


}
