namespace UtilityConsumtion.Calculations.Water;
public class WaterCalulatorOptions
{
    public decimal WaterPrice { get; set; }
    public decimal SavagePrice { get; set; }
    public decimal WaterSubscriptionFee { get; set; }
    public decimal WaterConsumptionFactor { get; set; }
    public decimal SavageSubscriptionFee { get; set; }
    public decimal SavageConsumptionFactor { get; set; }
    public decimal TaxRate { get; set; }

    public WaterCalulatorOptions(
        decimal waterPrice,
        decimal savagePrice,
        decimal waterSubscriptionFee,
        decimal waterConsumptionFactor,
        decimal savageSubscriptionFee,
        decimal savageConsumptionFactor,
        decimal taxRate)
    {
        WaterPrice = waterPrice;
        SavagePrice = savagePrice;
        WaterSubscriptionFee = waterSubscriptionFee;
        WaterConsumptionFactor = waterConsumptionFactor;
        SavageSubscriptionFee = savageSubscriptionFee;
        SavageConsumptionFactor = savageConsumptionFactor;
        TaxRate = taxRate;
    }

    public WaterCalulatorOptions()
    {
    }
}
