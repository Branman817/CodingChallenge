using Api.Dtos.Employee;
using Api.Models;

namespace Api.Mapper;

public static class EmployeeMapper
{
    public static GetEmployeeDto EmployeeToGetEmployeeDto(Employee employee)
    {
        var dto = new GetEmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Salary = employee.Salary,
            DateOfBirth = employee.DateOfBirth,
            Dependents = employee.Dependents.Select(x => DependentMapper.DependentToGetDependentDto(x)).ToList(),
        };

        return dto;
    }

    public static Employee GetEmployeeDtoToGetEmployee(GetEmployeeDto dto)
    {
        var employee = new Employee
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Salary = dto.Salary,
            DateOfBirth = dto.DateOfBirth,
            Dependents = dto.Dependents.Select(x => DependentMapper.GetDependentDtoToDependent(x)).ToList(),
        };

        return employee;
    }
}
