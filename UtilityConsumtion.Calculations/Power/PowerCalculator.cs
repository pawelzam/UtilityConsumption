using Microsoft.Extensions.Options;

namespace UtilityConsumtion.Calculations.Power;
public class PowerCalculator(IOptions<PowerCalulatorOptions> options)
{
    PowerCalulatorOptions powerCalulatorOptions = options.Value;
    public Usage Calculate(Metric previousMetric, Metric currentMetric, int billingUnitsCount)
    {
        var consumption = currentMetric.Consumption - previousMetric.Consumption;
        var activeEnergy = Math.Round(consumption * powerCalulatorOptions.ActiveEnergy, 2);
        var resFee = Math.Round(consumption * powerCalulatorOptions.RESFee, 2);
        var qualityComponent = Math.Round(consumption * powerCalulatorOptions.QualityComponent, 2);
        var networkFee = Math.Round(consumption * powerCalulatorOptions.NetworkFee, 2);
        var transitionFee = powerCalulatorOptions.TransitionFee * billingUnitsCount;
        var fixedTransmissionFee = powerCalulatorOptions.FixedTransmissionFee * billingUnitsCount;
        var subscriptionFee = powerCalulatorOptions.SubscriptionFee * billingUnitsCount;
        var commercialFee =  powerCalulatorOptions.CommercialFee * billingUnitsCount;
        var fixedCapacityFee = powerCalulatorOptions.FixedCapacityFee * billingUnitsCount;

        var netPrice = Math.Round(activeEnergy + resFee + qualityComponent + networkFee + transitionFee + fixedTransmissionFee + subscriptionFee + commercialFee + fixedCapacityFee, 2);
        var grossPrice = Math.Round(netPrice * powerCalulatorOptions.TaxRate, 2);
        return new Usage(netPrice, grossPrice);
    }
}
