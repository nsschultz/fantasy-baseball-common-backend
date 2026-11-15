using FantasyBaseball.Common.Enums;
using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Models;

public class BattingStatsTests
{
  [Fact]
  public void HasZeroDefaultValues()
  {
    var battingStats = new BattingStats();
    Assert.Equal(0, battingStats.AtBats);
    Assert.Equal(0, battingStats.Runs);
    Assert.Equal(0, battingStats.Hits);
    Assert.Equal(0, battingStats.Doubles);
    Assert.Equal(0, battingStats.Triples);
    Assert.Equal(0, battingStats.HomeRuns);
    Assert.Equal(0, battingStats.RunsBattedIn);
    Assert.Equal(0, battingStats.BaseOnBalls);
    Assert.Equal(0, battingStats.StrikeOuts);
    Assert.Equal(0, battingStats.StolenBases);
    Assert.Equal(0, battingStats.CaughtStealing);
    Assert.Equal(0, battingStats.TotalBases);
    Assert.Equal(0, battingStats.BattingAverage);
    Assert.Equal(0, battingStats.OnBasePercentage);
    Assert.Equal(0, battingStats.SluggingPercentage);
    Assert.Equal(0, battingStats.OnBasePlusSlugging);
    Assert.Equal(0, battingStats.ContactRate);
    Assert.Equal(0, battingStats.Power);
    Assert.Equal(0, battingStats.WalkRate);
    Assert.Equal(0, battingStats.Speed);
    Assert.Equal(0, battingStats.BasePerformanceValue);
  }

  [Fact]
  public void SetsAllProperties()
  {
    var battingStats = new BattingStats
    {
      StatsType = StatsType.CMBD,
      AtBats = 100,
      Runs = 25,
      Hits = 40,
      Doubles = 8,
      Triples = 2,
      HomeRuns = 10,
      RunsBattedIn = 35,
      BaseOnBalls = 12,
      StrikeOuts = 20,
      StolenBases = 5,
      CaughtStealing = 2,
      TotalBases = 72,
      BattingAverage = 0.4,
      OnBasePercentage = 0.45,
      SluggingPercentage = 0.72,
      OnBasePlusSlugging = 1.17,
      ContactRate = 80,
      Power = 150,
      WalkRate = 12,
      Speed = 105,
      BasePerformanceValue = 210
    };

    Assert.Equal(StatsType.CMBD, battingStats.StatsType);
    Assert.Equal(100, battingStats.AtBats);
    Assert.Equal(25, battingStats.Runs);
    Assert.Equal(40, battingStats.Hits);
    Assert.Equal(8, battingStats.Doubles);
    Assert.Equal(2, battingStats.Triples);
    Assert.Equal(10, battingStats.HomeRuns);
    Assert.Equal(35, battingStats.RunsBattedIn);
    Assert.Equal(12, battingStats.BaseOnBalls);
    Assert.Equal(20, battingStats.StrikeOuts);
    Assert.Equal(5, battingStats.StolenBases);
    Assert.Equal(2, battingStats.CaughtStealing);
    Assert.Equal(72, battingStats.TotalBases);
    Assert.Equal(0.4, battingStats.BattingAverage);
    Assert.Equal(0.45, battingStats.OnBasePercentage);
    Assert.Equal(0.72, battingStats.SluggingPercentage);
    Assert.Equal(1.17, battingStats.OnBasePlusSlugging);
    Assert.Equal(80, battingStats.ContactRate);
    Assert.Equal(150, battingStats.Power);
    Assert.Equal(12, battingStats.WalkRate);
    Assert.Equal(105, battingStats.Speed);
    Assert.Equal(210, battingStats.BasePerformanceValue);
  }

  [Theory]
  [InlineData(StatsType.YTD)]
  [InlineData(StatsType.PROJ)]
  [InlineData(StatsType.CMBD)]
  public void SupportsAllStatsTypes(StatsType statsType)
  {
    var battingStats = new BattingStats { StatsType = statsType };
    Assert.Equal(statsType, battingStats.StatsType);
  }

  [Fact]
  public void SetsNegativeValues()
  {
    var battingStats = new BattingStats
    {
      AtBats = -1,
      Power = -100.5
    };
    Assert.Equal(-1, battingStats.AtBats);
    Assert.Equal(-100.5, battingStats.Power);
  }

  [Fact]
  public void SetsMaxIntValues()
  {
    var battingStats = new BattingStats { AtBats = int.MaxValue };
    Assert.Equal(int.MaxValue, battingStats.AtBats);
  }

  [Fact]
  public void SetsMaxDoubleValues()
  {
    var battingStats = new BattingStats { BattingAverage = double.MaxValue };
    Assert.Equal(double.MaxValue, battingStats.BattingAverage);
  }
}