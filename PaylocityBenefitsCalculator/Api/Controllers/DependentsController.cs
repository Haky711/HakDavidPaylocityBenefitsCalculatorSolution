using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.CompilerServices;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    readonly IDependentService dependentService;
    
    public DependentsController(IDependentService dependentService)
    {
        this.dependentService = dependentService;
    }

    // Missing methods for CRUD operations should be added

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        return new ApiResponse<GetDependentDto>
        {
            Data = await this.dependentService.GetDependentByIdAsync(id),
            Success = true
        };
    }

    // Not sure if this is good practice, but this method make sense from my point of view
    [SwaggerOperation(Summary = "Get dependent by employee id")]
    [HttpGet("EmployeeId/{id}")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetByEmployeeId(int id)
    {
        return new ApiResponse<List<GetDependentDto>>
        {
            Data = await this.dependentService.GetDependentsByEmployeeIdAsync(id),
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Add dependent")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Add(PostDependentDto dependent)
    {
        var result = await this.dependentService.AddDependentAsync(dependent);
        return new ApiResponse<int>
        {
            Data = result,
            Success = result > 0 // Check if the result is greater than 0 to determine success, maybe change to an exception from service
        };
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        return new ApiResponse<List<GetDependentDto>>
        {
            Data = await this.dependentService.GetAllDependentsAsync(),
            Success = true
        };
    }
}
