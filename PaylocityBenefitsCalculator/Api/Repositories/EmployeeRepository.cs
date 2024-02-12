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
        var result = await _getEmployeesService.GetEmployees();

        var employeeDtos = result.Where(x => ValidDependencies(x)).ToList();

        return employeeDtos;
    }

    public async Task<GetEmployeeDto> GetEmployeeById(int id)
    {
        try
        {
            var employeeDto = await _getEmployeesService.GetEmployeeById(id);
            if(!ValidDependencies(employeeDto))
            {
                throw new Exception("Invalid employee, can't have more than 1 spouse or domestic partner or both");
            }
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

    // Checks that the employee only as 1 spouse or domestic partner (not both).  Written as its own metthod to be reusable code
    private bool ValidDependencies(GetEmployeeDto employee)
    {
        var partner = employee.Dependents.Where(x => x.Relationship == Relationship.Spouse || x.Relationship == Relationship.DomesticPartner).ToList();
        if (partner.Count > 1)
        {
            return false;
        }

        return true;
    }
}
