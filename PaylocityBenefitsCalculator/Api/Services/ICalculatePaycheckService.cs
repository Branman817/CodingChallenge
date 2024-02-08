using Api.Dtos.Employee;
using Api.Models;

namespace Api.Services;

public interface ICalculatePaycheckService
{
    public Paycheck GetEmployeePaycheck(GetEmployeeDto employee);
}
