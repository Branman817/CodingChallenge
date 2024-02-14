using Api.Dtos.Dependent;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentRepository _dependentRepository;

    public DependentsController(IDependentRepository dependentRepository)
    {
        _dependentRepository = dependentRepository;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        try
        {
            var dependent = await _dependentRepository.GetDependentByIdAsync(id);

            var result = new ApiResponse<GetDependentDto>
            {
                Data = dependent,
                Success = true
            };

            return result;
        }
        catch (Exception ex)
        {
            var dependentNotFound = new ApiResponse<GetDependentDto>
            {
                Message = ex.Message,
                Success = false
            };

            return NotFound(dependentNotFound);
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = await _dependentRepository.GetDependentsAsync();

        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = dependents.ToList(),
            Success = true
        };

        return result;
    }
}
