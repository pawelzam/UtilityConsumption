using Microsoft.Extensions.Options;

namespace UtilityConsumtion.Calculations.Water;
public class WaterCalculator(IOptions<WaterCalulatorOptions> options)
{
    WaterCalulatorOptions calulatorOptions = options.Value;
    public Usage Calculate(Metric previousMetric, Metric currentMetric)
    {
        var consumption = currentMetric.Consumption - previousMetric.Consumption;
        var waterSubscriptionFee =  calulatorOptions.WaterSubscriptionFee * calulatorOptions.WaterConsumptionFactor * calulatorOptions.TaxRate;
        var savageSubscriptionFee = calulatorOptions.SavageSubscriptionFee * calulatorOptions.SavageConsumptionFactor * calulatorOptions.TaxRate;

        var subscriptionFee = waterSubscriptionFee + savageSubscriptionFee;
        var waterConsumtionFee = consumption * (calulatorOptions.WaterPrice + (calulatorOptions.WaterPrice * (calulatorOptions.TaxRate - 1)));
        var savageConsumtionFee = consumption * (calulatorOptions.SavagePrice + (calulatorOptions.SavagePrice * (calulatorOptions.TaxRate - 1)));

        var netPrice = 0;
        var grossPrice = Math.Round(subscriptionFee + waterConsumtionFee + savageConsumtionFee, 2);
        return new Usage(netPrice, grossPrice);
    }
}
