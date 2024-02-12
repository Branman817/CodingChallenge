using Api.Dtos;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Mapper;
using Api.Models;
using Api.Services;

namespace Api.Repositories;

// Create a repository that will request the employee(s) data and process it.
public class EmployeeRepository : IEmployeeRepository
{
    private readonly ICalculatePaycheckService _calculatePaycheckService;
    private readonly IGetEmployeesService _getEmployeesService;
    public EmployeeRepository(ICalculatePaycheckService calculatePaycheckService, IGetEmployeesService getEmployeesService)
    {
        _calculatePaycheckService = calculatePaycheckService;
        _getEmployeesService = getEmployeesService;
    }

    public async Task<IEnumerable<GetEmployeeDto>> GetEmployees()
    {
        var employeeDtos = await _getEmployeesService.GetEmployees();
        return employeeDtos;
    }

    public async Task<GetEmployeeDto> GetEmployeeById(int id)
    {
        try
        {
            var employeeDto = await _getEmployeesService.GetEmployeeById(id);
            return employeeDto;
        }
        catch(Exception)
        {
            throw;
        }
    }

    // Create and call a separate service for calculating an employee's paycheck, abiding by Single Responsiblity Principle
    public async Task<PaycheckDto> GetEmployeePaycheck(int id)
    {
        try
        {
            var employee = await _getEmployeesService.GetEmployeeById(id);
            var paycheck = _calculatePaycheckService.GetEmployeePaycheck(employee);
            return paycheck;
        }
        catch
        {
            throw;
        }
    }
}
