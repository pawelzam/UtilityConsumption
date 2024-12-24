namespace UtilityConsumtion.Calculations.Power;
public class PowerCalulatorOptions
{
    public decimal ActiveEnergy { get; set; }
    public decimal RESFee { get; set; }
    public decimal QualityComponent { get; set; }
    public decimal NetworkFee { get; set; }
    public decimal TransitionFee { get; set; }
    public decimal FixedTransmissionFee { get; set; }
    public decimal SubscriptionFee { get; set; }
    public decimal CommercialFee { get; set; }
    public decimal FixedCapacityFee { get; set; }
    public decimal TaxRate { get; set; }

    public PowerCalulatorOptions(
        decimal activeEnergy,
        decimal resFee,
        decimal qualityComponent,
        decimal networkFee,
        decimal transitionFee,
        decimal fixedTransmissionFee,
        decimal subscriptionFee,
        decimal commercialFee,
        decimal fixedCapacityFee,
        decimal taxRate)
    {
        ActiveEnergy = activeEnergy;
        RESFee = resFee;
        QualityComponent = qualityComponent;
        NetworkFee = networkFee;
        TransitionFee = transitionFee;
        FixedTransmissionFee = fixedTransmissionFee;
        SubscriptionFee = subscriptionFee;
        CommercialFee = commercialFee;
        FixedCapacityFee = fixedCapacityFee;
        TaxRate = taxRate;
    }

    public PowerCalulatorOptions()
    {
    }
}
