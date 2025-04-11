using Microsoft.EntityFrameworkCore;
using MiniProjet.Projectmanagement.Contracts.Contracts.Responses;
using MiniProjet.ProjectManagement.Domain.Entites;
using MiniProjet.ProjectManagement.Infrastructure;

namespace MiniProjet.ProjectManagement.Services.Services.Employees;

public class EmployeeService : IEmployeeService
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

    public async Task<List<GetEmployeeResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .Select(e => new GetEmployeeResponse
            {
                Id = e.Id,
                Name = e.Name,
                LastName = e.LastName,
                Age = e.Age,
                DepartementName = e.Departement.Name
            })
            .ToListAsync(cancellationToken); // Implicit loading of Departement
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
