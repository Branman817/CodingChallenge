using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Services;

namespace Api.Repositories;

// Requesting Dependents is made as it's own repository instead of as part of the EmployeeRepository, while accessing EmployeeService
// I made this it's own Repository to abide by the Single Responsibility Principle
public class DependentRepository : IDependentRepository
{
    private readonly IGetEmployeesService _getEmployeesService;

    public DependentRepository(IGetEmployeesService getEmployeesService)
    {
        _getEmployeesService = getEmployeesService;
    }

    public async Task<IEnumerable<GetDependentDto>> GetDependentsAsync()
    {
        var employees = await _getEmployeesService.GetEmployees();

        var dependents = new List<GetDependentDto>();
        foreach (var employee in employees)
        {
            dependents.AddRange(employee.Dependents);
        }

        return dependents;
    }

    public async Task<GetDependentDto> GetDependentByIdAsync(int id)
    {
        var employees = await _getEmployeesService.GetEmployees();
        var dependents = employees.SelectMany(x => x.Dependents).ToList();

        var dependentById = dependents.Where(x => x.Id == id).FirstOrDefault();
        if(dependentById == null)
        {
            throw new Exception("Dependent Id not found");
        }

        return dependentById;
    }
}
