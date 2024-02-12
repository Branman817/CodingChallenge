namespace Api.Dtos;

public class PaycheckDto
{
    // value of paycheck after calculating total costs to be taken out of the base value
    public decimal Pay { get; set; }

    // Base value of the paycheck, evenly divided from an employee's base salary, before any benefit and dependent costs are taken out
    public decimal BaseValue { get; set; }

    public decimal BenefitCosts { get; set; }
}
