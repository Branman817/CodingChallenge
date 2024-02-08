using Api.Dtos.Employee;
using Api.Mapper;
using Api.Models;

namespace Api.Services;

public class CalculatePaycheckService : ICalculatePaycheckService
{
    // Create a service for calculating an employee's paycheck, abiding by Single Responsibility principle
    private const int _paychecksPerYear = 26;

    private const int _baseBenefitsCostPerMonth = 1000;

    private const int _dependentCost = 600;

    private const int _dependentOverFiftyAdditionalFee = 200;

    // For employees who make more than $80,000 per year,
    // incur an additional 2% of their yearly salary in benefit costs.
    private const decimal _additionalBenefitPercentile = 0.02m;

    public Paycheck GetEmployeePaycheck(GetEmployeeDto employee)
    {
        var paycheck = new Paycheck();
        paycheck.BaseValue = Math.Round(employee.Salary / _paychecksPerYear, 2);
        paycheck.BenefitCosts = _baseBenefitsCostPerMonth / 2;
        paycheck.Employee = EmployeeMapper.GetEmployeeDtoToGetEmployee(employee);

        if (employee.Dependents.Count > 0)
        {
            paycheck.BenefitCosts += AdditionalBenefitsCost(employee.Dependents.Select( x => DependentMapper.GetDependentDtoToDependent(x) ));
        }

        if(employee.Salary > 80000m)
        {
            paycheck.BenefitCosts += employee.Salary * _additionalBenefitPercentile;
        }

        paycheck.Pay = paycheck.BaseValue - paycheck.BenefitCosts;
        paycheck.Pay = Math.Round(paycheck.Pay, 2);

        return paycheck;
    }

    private decimal AdditionalBenefitsCost(IEnumerable<Dependent> dependents)
    {
        var benefitsCost = 0;

        benefitsCost += dependents.Select(x =>
        {
            var ageInDays = (DateTime.Today - x.DateOfBirth).Days;
            double age = ageInDays / 365;

            if(age > 50.0)
            {
                return _dependentCost + _dependentOverFiftyAdditionalFee;
            }
            return _dependentCost;
        }).Sum();

        return benefitsCost;
    }
}
