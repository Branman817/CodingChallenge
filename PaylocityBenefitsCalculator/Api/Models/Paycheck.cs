﻿namespace Api.Models;

public class Paycheck
{
    // value of paycheck after calculating total costs to be taken out of the base value
    public decimal Value { get; set; }

    // Base value of the paycheck, evenly divided from an employee's base salary, before any benefit and dependent costs are taken out
    public decimal BaseValue { get; set; }

    public decimal BenefitCosts { get; set; }

    public Employee Employee { get; set; }
}
