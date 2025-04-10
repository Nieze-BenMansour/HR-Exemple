using Microsoft.EntityFrameworkCore;
using MiniProjet.ProjectManagement.Infrastructure;
using MiniProjet.ProjectManagement.Services.Services.Departements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MiniProjetContext>(options =>
    options.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MiniProjetDb;Data Source=LAPTOP-UR7S8C4K;Encrypt=False;"));  

builder.Services.AddScoped<DepartementService>();



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
