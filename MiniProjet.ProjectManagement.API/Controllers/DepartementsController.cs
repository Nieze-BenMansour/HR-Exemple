using Microsoft.AspNetCore.Mvc;
using MiniProjet.ProjectManagement.API.Contracts.Requests;
using MiniProjet.ProjectManagement.API.Contracts.Responses;
using MiniProjet.ProjectManagement.Domain.Entites;
using MiniProjet.ProjectManagement.Services.Services.Departements;

namespace MiniProjet.ProjectManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartementsController : ControllerBase
{
    private readonly DepartementService _departementService;

    public DepartementsController(DepartementService departementService)
    {
        _departementService = departementService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartements()
    {
        var departements = await _departementService.GetAllAsync();

        var departementResponses = departements.Select(departements => new GetDepartementResponse
        {
            Id = departements.Id,
            Name = departements.Name,
            Description = departements.Description
        });

        return Ok(departementResponses);
    }

    [HttpGet("TestRoute/{id}")]
    public async Task<IActionResult> GetDepartementsByIdAsync(int id)
    {
        var departement = await _departementService.GetByIdAsync(id);
        
        if (departement == null)
        {
            return NotFound();
        }

        return Ok(departement);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartement(
        [FromBody] CreateDepartementRequest createDepartementRequest,
        CancellationToken cancellationToken)
    {
        if (createDepartementRequest == null)
        {
            return BadRequest();
        }

        int departementId = await _departementService.AddAsync(
            createDepartementRequest.Name,
            createDepartementRequest.Description,
            cancellationToken);

        return CreatedAtAction(nameof(GetDepartementsByIdAsync), new { id = departementId }, createDepartementRequest);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartement(
        int id,
        [FromBody] UpdateDepartementRequest updateDepartementRequest,
        CancellationToken cancellationToken)
    {
        if (updateDepartementRequest == null)
        {
            return BadRequest();
        }
        var departementToUpdate = await _departementService.GetByIdAsync(id, cancellationToken);
        if (departementToUpdate == null)
        {
            return NotFound();
        }
        departementToUpdate.Name = updateDepartementRequest.Name;
        departementToUpdate.Description = updateDepartementRequest.Description;

        await _departementService.UpdateAsync(departementToUpdate, cancellationToken);

        return NoContent();
    }
}
