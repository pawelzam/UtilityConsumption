namespace UtilityConsumtion.Calculations.Gas;
public class GasCalulatorOptions
{
    public decimal ConversionFactor { get; set; }
    public decimal SubscriptionFee { get; set; }
    public decimal FixedDiscributionFee { get; set; }
    public decimal VariableDistributionFee { get; set; }
    public decimal NetPricePerKWh { get; set; }
    public decimal TaxRate { get; set; }

    public GasCalulatorOptions(
        decimal conversionFactor,
        decimal subscriptionFee,
        decimal fixedDiscributionFee,
        decimal variableDistributionFee,
        decimal netPricePerKWh,
        decimal taxRate)
    {
        ConversionFactor = conversionFactor;
        SubscriptionFee = subscriptionFee;
        FixedDiscributionFee = fixedDiscributionFee;
        VariableDistributionFee = variableDistributionFee;
        NetPricePerKWh = netPricePerKWh;
        TaxRate = taxRate;
    }

    public GasCalulatorOptions()
    {
    }
}
