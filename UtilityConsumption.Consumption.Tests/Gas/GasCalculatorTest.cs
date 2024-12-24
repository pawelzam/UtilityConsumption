using FluentAssertions;
using Microsoft.Extensions.Options;
using UtilityConsumtion.Calculations;
using UtilityConsumtion.Calculations.Gas;

namespace UtilityConsumption.Consumption.Tests.Power
{
    [TestFixture]
    public class GasCalculatorTest
    {
        private GasCalculator _powerCalculator;

        [SetUp]
        public void Setup()
        {
            var options = Options.Create(new GasCalulatorOptions(
                conversionFactor: 11.574m,
                netPricePerKWh: 0.23965m,
                subscriptionFee: 8.67m,
                fixedDiscributionFee: 14.80m,
                variableDistributionFee: 0.03565m,
                taxRate: 1.23m
            ));
            _powerCalculator = new GasCalculator(options);
        }

        [Test]
        public void Calculate_ValidInputs_ReturnsCorrectPowerUsage()
        {
            // Arrange
            var previousMetric = new Metric(DateTime.Now.AddMonths(-1), 20);
            var currentMetric = new Metric(DateTime.Now, 372);
            int billingUnitsCount = 1;

            // Act
            var result = _powerCalculator.Calculate(previousMetric, currentMetric, billingUnitsCount);

            // Assert
            result.PriceNet.Should().Be(1145.04m);
            result.PriceGross.Should().Be(1408.40m);
        }
    }
}
