using Api.Models;

namespace Api.Services;

public class CalculatePaycheckService : ICalculatePaycheckService
{
    private const int _paychecksPerYear = 26;

    private const int _baseBenefitsCostPerMonth = 1000;

    private const int _dependentCost = 600;

    private const int _dependentOverFiftyAdditionalFee = 200;

    // For employees who make more than $80,000 per year,
    // incur an additional 2% of their yearly salary in benefit costs.
    private const decimal _additionalBenefitPercentile = 0.02m;

    public Paycheck GetEmployeePaycheck(Employee employee)
    {
        var paycheck = new Paycheck();
        paycheck.BaseValue = employee.Salary / _paychecksPerYear;
        paycheck.BenefitCosts = _baseBenefitsCostPerMonth / 2;
        paycheck.Employee = employee;

        if (employee.Dependents.Count > 0)
        {
            paycheck.BenefitCosts += AdditionalBenefitsCost(employee.Dependents);
        }

        if(employee.Salary > 80000m)
        {
            paycheck.BenefitCosts += employee.Salary * _additionalBenefitPercentile;
        }

        paycheck.Value = paycheck.BaseValue - paycheck.BenefitCosts;
        paycheck.Value = Math.Round(paycheck.Value, 2);

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
