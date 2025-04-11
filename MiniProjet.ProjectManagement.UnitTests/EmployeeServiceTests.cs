using Microsoft.EntityFrameworkCore;
using MiniProjet.ProjectManagement.Domain.Entites;
using MiniProjet.ProjectManagement.Infrastructure;
using MiniProjet.ProjectManagement.Services.Services.Employees;

namespace MiniProjet.ProjectManagement.Tests.Services
{
    public class EmployeeServiceTests
    {
        private MiniProjetContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<MiniProjetContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
                .Options;
            return new MiniProjetContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEmployeeToDatabase()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var department = new Departement { Id = 1, Name = "IT" };
            context.Departements.Add(department);
            await context.SaveChangesAsync();

            var service = new EmployeeService(context);
            var name = "John";
            var lastName = "Doe";
            var age = 30;
            var departmentId = 1;

            // Act
            await service.AddAsync(name, lastName, age, departmentId);

            // Assert
            var employee = await context.Employees.FirstOrDefaultAsync();
            Assert.NotNull(employee);
            Assert.Equal(name, employee.Name);
            Assert.Equal(lastName, employee.LastName);
            Assert.Equal(age, employee.Age);
            Assert.Equal(departmentId, employee.DepartementId);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEmployees()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var department = new Departement { Id = 1, Name = "IT" };
            context.Departements.Add(department);
            context.Employees.AddRange(
                new Employee { Id = 1, Name = "John", LastName = "Doe", Age = 30, DepartementId = 1 },
                new Employee { Id = 2, Name = "Jane", LastName = "Smith", Age = 25, DepartementId = 1 }
            );
            await context.SaveChangesAsync();

            var service = new EmployeeService(context);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, e => e.Name == "John" && e.DepartementName == "IT");
            Assert.Contains(result, e => e.Name == "Jane" && e.DepartementName == "IT");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var employee = new Employee { Id = 1, Name = "John", LastName = "Doe", Age = 30, DepartementId = 1 };
            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            var service = new EmployeeService(context);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Name, result.Name);
            Assert.Equal(employee.Id, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new EmployeeService(context);

            // Act
            var result = await service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEmployee()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var employee = new Employee { Id = 1, Name = "John", LastName = "Doe", Age = 30, DepartementId = 1 };
            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            var service = new EmployeeService(context);
            employee.Name = "Updated John";

            // Act
            await service.UpdateAsync(employee);

            // Assert
            var updatedEmployee = await context.Employees.FindAsync(1);
            Assert.NotNull(updatedEmployee);
            Assert.Equal("Updated John", updatedEmployee.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEmployee_WhenEmployeeExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var employee = new Employee { Id = 1, Name = "John", LastName = "Doe", Age = 30, DepartementId = 1 };
            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            var service = new EmployeeService(context);

            // Act
            await service.DeleteAsync(1);

            // Assert
            var deletedEmployee = await context.Employees.FindAsync(1);
            Assert.Null(deletedEmployee);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDoNothing_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new EmployeeService(context);

            // Act
            await service.DeleteAsync(999);

            // Assert
            Assert.Empty(context.Employees);
        }
    }
}