using Microsoft.Extensions.Options;

namespace UtilityConsumtion.Calculations.Gas;
public class GasCalculator(IOptions<GasCalulatorOptions> options)
{
    GasCalulatorOptions options = options.Value;

    public Usage Calculate(Metric previousMetric, Metric currentMetric, int billingUnitsCount)
    {
        var consumption = currentMetric.Consumption - previousMetric.Consumption;
        var consumptionInKWh = Math.Round(consumption * options.ConversionFactor, 0);
        var consumptionNet = consumptionInKWh * options.NetPricePerKWh;
        var subscriptionFee = options.SubscriptionFee * billingUnitsCount;
        var fixedDistributionFee = options.FixedDiscributionFee * billingUnitsCount;
        var variableDistributionFee = consumptionInKWh * options.VariableDistributionFee;

        var netPrice = Math.Round(consumptionNet + subscriptionFee + fixedDistributionFee + variableDistributionFee, 2);
        var grossPrice = Math.Round(netPrice * options.TaxRate, 2);
        return new Usage(netPrice, grossPrice);
    }
}
