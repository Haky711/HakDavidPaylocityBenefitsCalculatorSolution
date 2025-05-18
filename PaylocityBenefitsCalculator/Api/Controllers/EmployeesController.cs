using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeFacadeService employeeFacadeService;

    public EmployeesController(IEmployeeFacadeService employeeFacadeService)
    {
        this.employeeFacadeService = employeeFacadeService;
    }

    // Missing methods for CRUD operations should be added

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        return new ApiResponse<GetEmployeeDto>
        {
            Data = await this.employeeFacadeService.GetEmployeeWithDependentsByIdAsync(id),
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {        
        return new ApiResponse<List<GetEmployeeDto>>
        {
            Data = await this.employeeFacadeService.GetAllEmployeesWithDependentsAsync(),
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Add employee")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Add(PostEmployeeDto employee)
    {
        var result = await this.employeeFacadeService.AddEmployeeWithDependentsAsync(employee);
        return new ApiResponse<int>
        {
            Data = result,
            Success = result > 0 // Check if the result is greater than 0 to determine success, maybe change to an exception from service
        };
    }

    [SwaggerOperation(Summary = "Get employee paycheck")]
    [HttpGet("{id}/Paycheck")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> GetPaycheck(int id)
    {
        return new ApiResponse<GetPaycheckDto>
        {
            Data = await this.employeeFacadeService.GetPaycheckByEmployeeIdAsync(id),
            Success = true
        };
    }
}
