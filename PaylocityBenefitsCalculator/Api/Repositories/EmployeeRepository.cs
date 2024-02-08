using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Mapper;
using Api.Models;
using Api.Services;

namespace Api.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IEnumerable<Employee> _employees;
    private readonly ICalculatePaycheckService _calculatePaycheckService;
    public EmployeeRepository(ICalculatePaycheckService calculatePaycheckService)
    {
        _calculatePaycheckService = calculatePaycheckService;
        _employees = new List<Employee>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
    }

    public async Task<IEnumerable<GetEmployeeDto>> GetEmployees()
    {
        var result = _employees.Select(x => EmployeeMapper.EmployeeToGetEmployeeDto(x)).ToList();

        return result;
    }

    public async Task<GetEmployeeDto> GetEmployeeById(int id)
    {
        var employee = _employees.FirstOrDefault(x => x.Id == id);

        var employeeDto = EmployeeMapper.EmployeeToGetEmployeeDto(employee);

        return employeeDto;
    }

    public async Task<Paycheck> GetEmployeePaycheck(int id)
    {
        var employee = _employees.FirstOrDefault(x => x.Id == id);

        var paycheck = _calculatePaycheckService.GetEmployeePaycheck(employee);

        return paycheck;
    }
}
