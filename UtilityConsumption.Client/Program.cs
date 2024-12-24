using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using UtilityConsumption.Client;
using UtilityConsumtion.Calculations;
using UtilityConsumtion.Calculations.Gas;
using UtilityConsumtion.Calculations.Power;
using UtilityConsumtion.Calculations.Water;

var configuration = new ConfigurationBuilder()
                   .SetBasePath(AppContext.BaseDirectory)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();

// Setup DI
var serviceProvider = new ServiceCollection()
    .Configure<PowerCalulatorOptions>(configuration.GetSection("PowerCalulatorOptions"))
    .Configure<GasCalulatorOptions>(configuration.GetSection("GasCalulatorOptions"))
    .Configure<WaterCalulatorOptions>(configuration.GetSection("WaterCalulatorOptions"))
    .AddSingleton<PowerCalculator>()
    .AddSingleton<GasCalculator>()
    .AddSingleton<WaterCalculator>()
    .BuildServiceProvider();

var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    PrepareHeaderForMatch = args => args.Header.ToLower(),
   
};

var local1 = ReadMetricts("Metrics_Mazowiecka2BD_1.csv");
var local2 = ReadMetricts("Metrics_Mazowiecka2BD_2.csv");

Console.WriteLine($"Mazowiecka 2BD lokal 1:");
Console.WriteLine($"Prąd: {local1.Item1 - local2.Item1} zł");
Console.WriteLine($"Gaz: {local1.Item2 * 0.6m} zł (60%)");
Console.WriteLine($"Woda: {local1.Item3}  zł");

Console.WriteLine("-------------------------------------------------");

Console.WriteLine($"Mazowiecka 2BD lokal 2:");
Console.WriteLine($"Prąd: {local2.Item1} zł");
Console.WriteLine($"Gaz: {local2.Item2 * 0.4m}  zł (40%)");
Console.WriteLine($"Woda: {local2.Item3}  zł");


(decimal, decimal, decimal) ReadMetricts(string metrics)
{
    using var reader = new StreamReader(metrics);
    using var csv = new CsvReader(reader, config);
    var records = csv.GetRecords<Metrics>();
    var lastTwo = records.TakeLast(2).ToArray();

    var previous = lastTwo.First();
    var current = lastTwo.Last();
    var powerCalculator = serviceProvider.GetService<PowerCalculator>();

    var powerConsumption = powerCalculator!.Calculate(new Metric(previous.Date, previous.Power), new Metric(current.Date, current.Power), 1);
    var gasConsumption = serviceProvider.GetService<GasCalculator>()!.Calculate(new Metric(previous.Date, previous.Gas), new Metric(current.Date, current.Gas), 1);
    var waterConsumption = serviceProvider.GetService<WaterCalculator>()!.Calculate(new Metric(previous.Date, previous.Water), new Metric(current.Date, current.Water));

    return (powerConsumption.PriceGross, gasConsumption.PriceGross, waterConsumption.PriceGross);
}

