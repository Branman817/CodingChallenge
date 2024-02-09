using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeesController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            var employee = await _employeeRepository.GetEmployeeById(id);

            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = employee,
                Success = true,
            };

            return result;
        }
        catch(Exception ex)
        {
            var employeeNotFound = new ApiResponse<GetEmployeeDto>
            {
                Message = ex.Message,
                Success = false
            };
            return NotFound(employeeNotFound);
        }
        
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var employees = await _employeeRepository.GetEmployees();

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees.ToList(),
            Success = true
        };

        return result;
    }

    // Request paycheck by employee id, so that it can easily return NotFound for nonexistent employees
    [SwaggerOperation(Summary = "View Employee Paycheck")]
    [HttpGet("{id}/paycheck")]
    public async Task<ActionResult<ApiResponse<Paycheck>>> GetEmployeePaycheck(int id)
    {
        try
        {
            var paycheck = await _employeeRepository.GetEmployeePaycheck(id);

            var result = new ApiResponse<Paycheck>
            {
                Data = paycheck,
                Success = true
            };

            return result;
        }
        catch(Exception ex)
        {
            var employeeNotFound = new ApiResponse<Paycheck>
            {
                Message = ex.Message,
                Success = false
            };
            return NotFound(employeeNotFound);
        }
    }
}
