using FantasyBaseball.Common.Enums;
using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Models;

public class PitchingStatsTest
{
  [Fact]
  public void SetsAllProperties()
  {
    var pitchingStats = new PitchingStats
    {
      StatsType = StatsType.PROJ,
      Wins = 12,
      Losses = 7,
      QualityStarts = 18,
      Saves = 2,
      BlownSaves = 1,
      Holds = 4,
      InningsPitched = 180.2,
      HitsAllowed = 150,
      EarnedRuns = 62,
      HomeRunsAllowed = 18,
      BaseOnBallsAllowed = 48,
      StrikeOuts = 205,
      FlyBallRate = 35.5,
      GroundBallRate = 44.2,
      EarnedRunAverage = 3.1,
      WalksAndHitsPerInningPitched = 1.1,
      BattingAverageOnBallsInPlay = 0.289,
      StrandRate = 73.4,
      Command = 4.3,
      Dominance = 10.2,
      Control = 2.4,
      GroundBallToFlyBallRate = 1.25,
      BasePerformanceValue = 125
    };

    Assert.Equal(StatsType.PROJ, pitchingStats.StatsType);
    Assert.Equal(12, pitchingStats.Wins);
    Assert.Equal(7, pitchingStats.Losses);
    Assert.Equal(18, pitchingStats.QualityStarts);
    Assert.Equal(2, pitchingStats.Saves);
    Assert.Equal(1, pitchingStats.BlownSaves);
    Assert.Equal(4, pitchingStats.Holds);
    Assert.Equal(180.2, pitchingStats.InningsPitched);
    Assert.Equal(150, pitchingStats.HitsAllowed);
    Assert.Equal(62, pitchingStats.EarnedRuns);
    Assert.Equal(18, pitchingStats.HomeRunsAllowed);
    Assert.Equal(48, pitchingStats.BaseOnBallsAllowed);
    Assert.Equal(205, pitchingStats.StrikeOuts);
    Assert.Equal(35.5, pitchingStats.FlyBallRate);
    Assert.Equal(44.2, pitchingStats.GroundBallRate);
    Assert.Equal(3.1, pitchingStats.EarnedRunAverage);
    Assert.Equal(1.1, pitchingStats.WalksAndHitsPerInningPitched);
    Assert.Equal(0.289, pitchingStats.BattingAverageOnBallsInPlay);
    Assert.Equal(73.4, pitchingStats.StrandRate);
    Assert.Equal(4.3, pitchingStats.Command);
    Assert.Equal(10.2, pitchingStats.Dominance);
    Assert.Equal(2.4, pitchingStats.Control);
    Assert.Equal(1.25, pitchingStats.GroundBallToFlyBallRate);
    Assert.Equal(125, pitchingStats.BasePerformanceValue);
  }
}
