using Api.Dtos.Employee;
using Api.Mapper;
using Api.Models;

namespace Api.Services;

public class CalculatePaycheckService : ICalculatePaycheckService
{
    // Create a service for calculating an employee's paycheck, abiding by Single Responsibility principle
    private const int _paychecksPerYear = 26;

    private const decimal _baseBenefitsCostPerMonth = 1000m;

    private const decimal _dependentCost = 600m;

    private const decimal _dependentOverFiftyAdditionalFee = 200m;

    // For employees who make more than $80,000 per year,
    // incur an additional 2% of their yearly salary in benefit costs.
    private const decimal _additionalBenefitPercentile = 0.02m;

    public Paycheck GetEmployeePaycheck(GetEmployeeDto employee)
    {
        var paycheck = new Paycheck();
        paycheck.BaseValue = Math.Round(employee.Salary / _paychecksPerYear, 2);

        // Calculate the monthly benefits costs, multiply it by 12 to get the yearly benefits cost, and then divide that result by the number of paychecks per year
        var benefitsCostPerMonth = _baseBenefitsCostPerMonth;
        if (employee.Dependents.Count > 0)
        {
            benefitsCostPerMonth += AdditionalBenefitsCost(employee.Dependents.Select(x => DependentMapper.GetDependentDtoToDependent(x)));
        }
        if (employee.Salary > 80000m)
        {
            benefitsCostPerMonth += (employee.Salary * _additionalBenefitPercentile) / 12;
        }
        var benefitsCostPerYear = benefitsCostPerMonth * 12;
        var benefitsCostPerPaycheck = benefitsCostPerYear / _paychecksPerYear;
        paycheck.BenefitCosts = Math.Round(benefitsCostPerPaycheck, 2);

        paycheck.Pay = paycheck.BaseValue - paycheck.BenefitCosts;
        paycheck.Pay = Math.Round(paycheck.Pay, 2);

        return paycheck;
    }

    // Calculate the additional benefits costs per dependent in a separate method, to keep the code clean, organized, and easy to read
    private decimal AdditionalBenefitsCost(IEnumerable<Dependent> dependents)
    {
        var benefitsCost = 0m;

        benefitsCost += dependents.Select(x =>
        {
            decimal ageInDays = (DateTime.Today - x.DateOfBirth).Days;
            var age = ageInDays / 365;

            if(age > 50.0m)
            {
                return _dependentCost + _dependentOverFiftyAdditionalFee;
            }
            return _dependentCost;
        }).Sum();

        return benefitsCost;
    }
}
