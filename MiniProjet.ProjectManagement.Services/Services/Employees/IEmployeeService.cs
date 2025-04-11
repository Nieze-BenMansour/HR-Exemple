using MiniProjet.ProjectManagement.Domain.Entites;

namespace MiniProjet.ProjectManagement.Services.Services.Employees;

public interface IEmployeeService
{
    Task AddAsync(string name, string lastName, int age, int departmentId, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
}