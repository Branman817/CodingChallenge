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
        var result = await _getEmployeesService.GetEmployees();

        var employeeDtos = result.Where(x => ValidDependencies(x)).ToList();

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
    public async Task<Paycheck> GetEmployeePaycheck(int id)
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

    private bool ValidDependencies(GetEmployeeDto employee)
    {
        // check that there is only 1 spouse or domestic partner (not both)
        var partner = employee.Dependents.Where(x => x.Relationship == Relationship.Spouse || x.Relationship == Relationship.DomesticPartner).ToList();
        if (partner.Count > 1)
        {
            return false;
        }

        return true;
    }
}
