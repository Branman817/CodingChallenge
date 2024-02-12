using Api.Dtos;
using Api.Dtos.Employee;

namespace Api.Services;

public interface ICalculatePaycheckService
{
    public PaycheckDto GetEmployeePaycheck(GetEmployeeDto employee);
}
