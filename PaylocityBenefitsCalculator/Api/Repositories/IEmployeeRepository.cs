using Api.Dtos.Employee;
using Api.Models;

namespace Api.Repositories;

public interface IEmployeeRepository
{
    public Task<IEnumerable<GetEmployeeDto>> GetEmployees();

    public Task<GetEmployeeDto> GetEmployeeById(int id);

    public Task<Paycheck> GetEmployeePaycheck(int id);
}
