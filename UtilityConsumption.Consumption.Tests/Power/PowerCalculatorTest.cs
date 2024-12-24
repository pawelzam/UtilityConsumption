using FluentAssertions;
using Microsoft.Extensions.Options;
using UtilityConsumtion.Calculations;
using UtilityConsumtion.Calculations.Power;

namespace UtilityConsumption.Consumption.Tests.Power;

[TestFixture]
public class PowerCalculatorTest
{
    private PowerCalculator _powerCalculator;

    [SetUp]
    public void Setup()
    {
        var options = Options.Create(new PowerCalulatorOptions(
            activeEnergy: 0.414m,
            resFee: 0.009m,
            qualityComponent: 0.0095m,
            networkFee: 0.2223m,
            transitionFee: 0.35m,
            fixedTransmissionFee: 6.56m,
            subscriptionFee: 0.75m,
            commercialFee: 4.06m,
            fixedCapacityFee: 10.64m,
            taxRate: 1.23m
        ));

        _powerCalculator = new PowerCalculator(options);
    }

    [Test]
    public void Calculate_ValidInputs_ReturnsCorrectPowerUsage()
    {
        // Arrange
        var previousMetric = new Metric(DateTime.Now.AddMonths(-1), 206);
        var currentMetric = new Metric(DateTime.Now, 530);
        int billingUnitsCount = 1;

        // Act
        var result = _powerCalculator.Calculate(previousMetric, currentMetric, billingUnitsCount);

        // Assert
        result.PriceNet.Should().Be(234.53m);
        result.PriceGross.Should().Be(288.47m);
    }
}
