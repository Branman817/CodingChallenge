using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Mapper;
using Api.Models;
using Api.Services;

namespace Api.Repositories;

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

        //var employees = employeeDtos.Select(x =>
        //{

        //});

        return employeeDtos;
    }

    public async Task<GetEmployeeDto> GetEmployeeById(int id)
    {
        var employeeDto = await _getEmployeesService.GetEmployeeById(id);

        return employeeDto;
    }

    // Create and call a separate service for calculating an employee's paycheck, abiding by Single Responsiblity Principle
    public async Task<Paycheck> GetEmployeePaycheck(int id)
    {
        var employee = await _getEmployeesService.GetEmployeeById(id);

        var paycheck = _calculatePaycheckService.GetEmployeePaycheck(employee);

        return paycheck;
    }
}
