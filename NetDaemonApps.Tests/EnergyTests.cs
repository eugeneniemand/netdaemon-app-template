using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using HomeAssistantGenerated;
using Microsoft.Extensions.Logging;
using Moq;
using NetDaemon.HassModel.Entities;
using NetDaemon.HassModel.Mocks.Moq;
using NetDaemonApps.Tests.Helpers;
using Niemand;
using Niemand.Energy;
using Niemand.Helpers;

namespace NetDaemonApps.Tests;

public class EnergyTests
{
    private class RateInfo
    {
        public DateTime StartDate;
        public double Rate;
    }

    [Test]
    public void TestRatesAtSpecifiedTime()
    {
        var ctx = new AppTestContext();
        ctx.Scheduler.AdvanceTo(new DateTime(2022, 11, 02, 5,0,0).Ticks);
        var Rates = ctx.GetEntity<Entity>("octopusagile.all_rates");

        var ratesDict = new SortedDictionary<DateTime, double>();
        // Arrange
        var lines = File.ReadAllLines("agile_rates_2.txt");
        foreach (var line in lines)
        {
            var rateArray = line.Split(": ");
            ratesDict.Add(DateTime.Parse(rateArray[0]), Double.Parse(rateArray[1]));
        }
        ctx.TriggerStateChange(Rates, "", ratesDict);

        var app = ctx.InitEnergy();

        var message = EnergyApp.GetRatesMessageText(app.Rates.CheapestWindows());
        message.Should().Be("Cheapest Rates(AVG):\n```\n10:30 - 14.62 p/kWh (3 hrs)\n11:00 - 14.30 p/kWh (2 hrs)\n11:30 - 13.93 p/kWh (1 hr)```");
    }

    [Test]
    public void TestRatesOnFullDataSet()
    {
        var ctx = new AppTestContext();
        var Rates = ctx.GetEntity<Entity>("octopusagile.all_rates");

        var ratesDict = new SortedDictionary<DateTime, double>();
        // Arrange
        var lines = File.ReadAllLines("agile_rates_2.txt");
        foreach (var line in lines)
        {
            var rateArray = line.Split(": ");
            ratesDict.Add(DateTime.Parse(rateArray[0]), Double.Parse(rateArray[1]));
        }
        ctx.TriggerStateChange(Rates, "", ratesDict);

        var app = ctx.InitEnergy();

        var message = EnergyApp.GetRatesMessageText(app.Rates.CheapestWindows());
        message.Should().Be("Cheapest Rates(AVG):\n```\n02:00 - 8.45 p/kWh (3 hrs)\n02:30 - 7.77 p/kWh (2 hrs)\n03:00 - 7.08 p/kWh (1 hr)```");
    }
    
    [Test]
    public void TestRealRates()
    {
       
        var Rates = new SortedDictionary<DateTime, double>();
        // Arrange
        var lines = File.ReadAllLines("agile_rates_2.txt");
        foreach (var line in lines)
        {
            var rateArray = line.Split(": ");
            Rates.Add( DateTime.Parse(rateArray[0]),  Double.Parse(rateArray[1]));
        }
        
        // Act
        var oneHourAvg = Rates.WindowedAverageLeft(2).MinWithKey();
        var twoHourAvg = Rates.WindowedAverageLeft(4).MinWithKey();
        var threeHourAvg = Rates.WindowedAverageLeft(6).MinWithKey();

        // Assert
        Math.Round(oneHourAvg.Value,2).Should().Be(7.08d);
        Math.Round(twoHourAvg.Value, 2).Should().Be(7.77d);
        Math.Round(threeHourAvg.Value, 2).Should().Be(8.45d);
    }


    [Test]
    public void WindowedAverageLeftReturnsTheSlidingAverageOfEveryTwoItems()
    {
       
        var input = new SortedDictionary<DateTime, double>();
        // Arrange
        input.Add(new DateTime(2022, 10, 01), 1);
        input.Add(new DateTime(2022, 10, 02), 2);
        input.Add(new DateTime(2022, 10, 03), 3);
        input.Add(new DateTime(2022, 10, 04), 4);
        input.Add(new DateTime(2022, 10, 05), 5);
        input.Add(new DateTime(2022, 10, 06), 6);
        input.Add(new DateTime(2022, 10, 07), 7);

        // Act
        var avgs = input.WindowedAverageLeft(2);

        // Assert
        avgs.Count.Should().Be(6);
        avgs[new DateTime(2022, 10, 01)].Should().Be(1.5);
        avgs[new DateTime(2022, 10, 02)].Should().Be(2.5);
        avgs[new DateTime(2022, 10, 03)].Should().Be(3.5);
        avgs[new DateTime(2022, 10, 04)].Should().Be(4.5);
        avgs[new DateTime(2022, 10, 05)].Should().Be(5.5);
        avgs[new DateTime(2022, 10, 06)].Should().Be(6.5);
    }

    [Test]
    public void WindowedAverageLeftReturnsTheSlidingAverageOfEveryThreeItems()
    {

        var input = new SortedDictionary<DateTime, double>();
        // Arrange
        input.Add(new DateTime(2022, 10, 01), 1);
        input.Add(new DateTime(2022, 10, 02), 2);
        input.Add(new DateTime(2022, 10, 03), 3);
        input.Add(new DateTime(2022, 10, 04), 4);
        input.Add(new DateTime(2022, 10, 05), 5);
        input.Add(new DateTime(2022, 10, 06), 6);
        input.Add(new DateTime(2022, 10, 07), 7);

        // Act
        var avgs = input.WindowedAverageLeft(3);

        // Assert
        avgs.Count.Should().Be(5);
        avgs[new DateTime(2022, 10, 01)].Should().Be(2);
        avgs[new DateTime(2022, 10, 02)].Should().Be(3);
        avgs[new DateTime(2022, 10, 03)].Should().Be(4);
        avgs[new DateTime(2022, 10, 04)].Should().Be(5);
        avgs[new DateTime(2022, 10, 05)].Should().Be(6);
    }

    [Test]
    public void WindowedAverageLeftReturnsTheSlidingAverageOfEveryFourItems()
    {

        var input = new SortedDictionary<DateTime, double>();
        // Arrange
        input.Add(new DateTime(2022, 10, 01), 1);
        input.Add(new DateTime(2022, 10, 02), 2);
        input.Add(new DateTime(2022, 10, 03), 3);
        input.Add(new DateTime(2022, 10, 04), 4);
        input.Add(new DateTime(2022, 10, 05), 5);
        input.Add(new DateTime(2022, 10, 06), 6);
        input.Add(new DateTime(2022, 10, 07), 7);

        // Act
        var avgs = input.WindowedAverageLeft(4);

        // Assert
        avgs.Count.Should().Be(4);
        avgs[new DateTime(2022, 10, 01)].Should().Be(2.5);
        avgs[new DateTime(2022, 10, 02)].Should().Be(3.5);
        avgs[new DateTime(2022, 10, 03)].Should().Be(4.5);
        avgs[new DateTime(2022, 10, 04)].Should().Be(5.5);
    }

    [Test]
    public void MinWithKeyReturnsTheKeyAndValueOfTheItemWithTheMinimumValue()
    {
        var input = new SortedDictionary<DateTime, double>();
        // Arrange
        input.Add(new DateTime(2022, 10, 01), 10);
        input.Add(new DateTime(2022, 10, 02), 8);
        input.Add(new DateTime(2022, 10, 03), 6);
        input.Add(new DateTime(2022, 10, 04), 2);
        input.Add(new DateTime(2022, 10, 05), 5);
        input.Add(new DateTime(2022, 10, 06), 6);
        input.Add(new DateTime(2022, 10, 07), 7);

        // Act
        var valuePair = input.MinWithKey();

        // Assert
        valuePair.Key.Should().Be(new DateTime(2022, 10, 04));
        valuePair.Value.Should().Be(2);
    }

    

}

public static class EnergyAppTestContextInstanceExtensions
{
    public static EnergyApp InitEnergy(this AppTestContext ctx) => new(ctx.HaContext, ctx.Scheduler, new Mock<ILogger<EnergyApp>>().Object, new Mock<IServices>().Object);
}