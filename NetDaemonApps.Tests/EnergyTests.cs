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
    public void DishwasherStateShouldBeCleanAfterPowerDecreasesForTheFourthTime()
    {
        var _ctx = new AppTestContext();
        var Rates = new SortedDictionary<DateTime, double>();
        // Arrange
        var lines = File.ReadAllLines("agile_rates.txt");
        foreach (var line in lines)
        {
            var rateArray = line.Split(": ");
            Rates.Add( DateTime.Parse(rateArray[0]),  Double.Parse(rateArray[1]));
        }
        var cheapestRates = Rates.CheapestWindows();
                        
        _ctx.InitEnergy();
        // Act
        

        // Assert
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
    public void MinWithKeyReturnsTheKeyAndValueOfTheItemWIthTheMinimumValue()
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
    public static EnergyApp InitEnergy(this AppTestContext ctx) => new(ctx.HaContext, ctx.Scheduler, new Mock<ILogger<EnergyApp>>().Object, new Mock<Services>().Object);
}