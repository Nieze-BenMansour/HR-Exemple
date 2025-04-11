using Microsoft.EntityFrameworkCore;
using MiniProjet.ProjectManagement.Infrastructure;
using MiniProjet.ProjectManagement.Services.Services.Departements;
using MiniProjet.ProjectManagement.Services.Services.Employees;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MiniProjetContext>(options =>
    options.UseSqlServer(connectionString));  

builder.Services.AddScoped<IDepartementService, DepartementService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
