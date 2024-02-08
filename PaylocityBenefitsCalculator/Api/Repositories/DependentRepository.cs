using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Services;

namespace Api.Repositories;

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

        return dependentById;
    }
}
