using FantasyBaseball.Common.Enums;
using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Models;

public class BattingStatsTest
{
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
      ContractRate = 80,
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
    Assert.Equal(80, battingStats.ContractRate);
    Assert.Equal(150, battingStats.Power);
    Assert.Equal(12, battingStats.WalkRate);
    Assert.Equal(105, battingStats.Speed);
    Assert.Equal(210, battingStats.BasePerformanceValue);
  }
}