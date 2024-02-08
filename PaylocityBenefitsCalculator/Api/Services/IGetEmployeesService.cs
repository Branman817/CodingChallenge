using Api.Dtos.Employee;

namespace Api.Services;

public interface IGetEmployeesService
{
    public Task<IEnumerable<GetEmployeeDto>> GetEmployees();

    public Task<GetEmployeeDto> GetEmployeeById(int id);
}
