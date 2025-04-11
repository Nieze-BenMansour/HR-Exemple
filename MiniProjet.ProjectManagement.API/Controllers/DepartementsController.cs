using Microsoft.AspNetCore.Mvc;
using MiniProjet.ProjectManagement.API.Contracts.Requests;
using MiniProjet.ProjectManagement.API.Contracts.Responses;
using MiniProjet.ProjectManagement.Domain.Entites;
using MiniProjet.ProjectManagement.Services.Services.Departements;

namespace MiniProjet.ProjectManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartementsController(
    IDepartementService _departementService,
    ILogger<DepartementsController> _logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDepartements()
    {
        _logger.LogInformation("Fetching all departements from the database.");

        var departements = await _departementService.GetAllAsync();

        var departementResponses = departements.Select(departements => new GetDepartementResponse
        {
            Id = departements.Id,
            Name = departements.Name,
            Description = departements.Description
        });

        _logger.LogInformation("Fetched {Count} departements from the database.", departementResponses.Count());

        return Ok(departementResponses);
    }

    [HttpGet("TestRoute/{id}")]
    public async Task<IActionResult> GetDepartementsByIdAsync(int id)
    {
        _logger.LogInformation("Fetching departement with ID {Id} from the database.", id);

        var departement = await _departementService.GetByIdAsync(id);

        if (departement == null)
        {
            _logger.LogWarning("Departement with ID {Id} not found.", id);
            return NotFound();
        }

        _logger.LogInformation("Fetched departement with ID {Id} from the database.", id);

        return Ok(departement);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartement(
        [FromBody] CreateDepartementRequest createDepartementRequest,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new departement with name {Name}.", createDepartementRequest.Name);

        if (createDepartementRequest == null)
        {
            _logger.LogWarning("CreateDepartementRequest is null.");
            return BadRequest();
        }

        int departementId = await _departementService.AddAsync(
            createDepartementRequest.Name,
            createDepartementRequest.Description,
            cancellationToken);

        _logger.LogInformation("Created a new departement with ID {Id}.", departementId);

        return CreatedAtAction(nameof(GetDepartementsByIdAsync), new { id = departementId }, createDepartementRequest);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartement(
        int id,
        [FromBody] UpdateDepartementRequest updateDepartementRequest,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating departement with ID {Id}.", id);
        if (updateDepartementRequest == null)
        {
            _logger.LogWarning("UpdateDepartementRequest is null.");
            return BadRequest();
        }
        var departementToUpdate = await _departementService.GetByIdAsync(id, cancellationToken);
        if (departementToUpdate == null)
        {
            _logger.LogWarning("Departement with ID {Id} not found.", id);
            return NotFound();
        }
        departementToUpdate.Name = updateDepartementRequest.Name;
        departementToUpdate.Description = updateDepartementRequest.Description;

        await _departementService.UpdateAsync(departementToUpdate, cancellationToken);

        _logger.LogInformation("Updated departement with ID {Id}.", id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartement(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting departement with ID {Id}.", id);
        var departementToDelete = await _departementService.GetByIdAsync(id, cancellationToken);
        if (departementToDelete == null)
        {
            _logger.LogWarning("Departement with ID {Id} not found.", id);
            return NotFound();
        }
        await _departementService.DeleteAsync(id, cancellationToken);
        _logger.LogInformation("Deleted departement with ID {Id}.", id);
        return NoContent();
    }
}
