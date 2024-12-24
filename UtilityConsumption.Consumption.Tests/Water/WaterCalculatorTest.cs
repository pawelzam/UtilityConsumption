using FluentAssertions;
using Microsoft.Extensions.Options;
using UtilityConsumtion.Calculations;
using UtilityConsumtion.Calculations.Water;

namespace UtilityConsumption.Consumption.Tests.Water;

[TestFixture]
public class WaterCalculatorTest
{
    private WaterCalculator _WaterCalculator;

    [SetUp]
    public void Setup()
    {
        var options = Options.Create(new WaterCalulatorOptions(
            waterPrice: 4.59m,
            savagePrice: 7.56m,
            waterSubscriptionFee: 9.53m,
            waterConsumptionFactor: 0.9m,
            savageSubscriptionFee: 10.01m,
            savageConsumptionFactor: 0.9m,
            taxRate: 1.08m
        ));

        _WaterCalculator = new WaterCalculator(options);
    }

    [Test]
    public void Calculate_ValidInputs_ReturnsCorrectWaterUsage()
    {
        // Arrange
        var previousMetric = new Metric(DateTime.Now.AddMonths(-1), 0);
        var currentMetric = new Metric(DateTime.Now, 26);
        
        // Act
        var result = _WaterCalculator.Calculate(previousMetric, currentMetric);

        // Assert
        result.PriceNet.Should().Be(0);
        result.PriceGross.Should().Be(360.16m);
    }
}
