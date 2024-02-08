using Api.Models;

namespace Api.Services;

public interface ICalculatePaycheckService
{
    public Paycheck GetEmployeePaycheck(Employee employee);
}
