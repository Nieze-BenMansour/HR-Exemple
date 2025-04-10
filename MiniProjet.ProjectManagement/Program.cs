using MiniProjet.ProjectManagement.Domain.Entites;
using MiniProjet.ProjectManagement.Infrastructure;
using MiniProjet.ProjectManagement.Services.Services.Departements;



var employeeService = new EmployeeService(new MiniProjetContext());

await employeeService.AddAsync(name: "John",lastName: "Doe",age: 30, departmentId: 1);
await employeeService.AddAsync("Jane", "Smith", 25, 2);
await employeeService.AddAsync("Alice", "Johnson", 28, 1);
await employeeService.AddAsync("Bob", "Brown", 35, 2);

var employees = await employeeService.GetAllAsync();

foreach (var employee in employees)
{
    Console.WriteLine($"Name: {employee.Name}, Department: {employee.Departement.Name}");
}