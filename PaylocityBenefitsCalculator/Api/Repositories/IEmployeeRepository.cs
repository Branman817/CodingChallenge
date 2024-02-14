using Api.Dtos.Employee;

namespace Api.Repositories;

public interface IEmployeeRepository
{
    public Task<IEnumerable<GetEmployeeDto>> GetEmployees();

    public Task<GetEmployeeDto> GetEmployeeById(int id);

    public Task<GetPaycheckDto> GetEmployeePaycheck(int id);
}
