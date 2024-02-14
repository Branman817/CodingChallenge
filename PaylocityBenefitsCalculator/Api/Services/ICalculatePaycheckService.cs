using Api.Dtos.Employee;

namespace Api.Services;

public interface ICalculatePaycheckService
{
    public GetPaycheckDto GetEmployeePaycheck(GetEmployeeDto employee);
}
