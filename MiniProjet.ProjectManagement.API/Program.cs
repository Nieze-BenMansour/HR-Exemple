using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MiniProjet.ProjectManagement.API;
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

builder.Services.AddSingleton<IExceptionHandler, GlobalExceptionHandler>(); // Register the global exception handler

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

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        if (exception != null)
        {
            var globalExceptionHandler = context.RequestServices.GetRequiredService<GlobalExceptionHandler>();
            await globalExceptionHandler.HandleExceptionAsync(context, exception);
        }
    });
});


app.Run();
