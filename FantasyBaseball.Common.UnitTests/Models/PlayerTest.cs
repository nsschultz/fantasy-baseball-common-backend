using System;
using FantasyBaseball.Common.Enums;
using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Models;

public class PlayerTest
{
  [Fact]
  public void HasEmptyCollectionsByDefault()
  {
    var player = new Player();
    Assert.Empty(player.BattingStats);
    Assert.Empty(player.PitchingStats);
    Assert.Empty(player.Positions);
  }

  [Fact]
  public void SetsAllProperties()
  {
    var id = Guid.NewGuid();
    var battingStats = new BattingStats { StatsType = StatsType.YTD, Hits = 50 };
    var pitchingStats = new PitchingStats { StatsType = StatsType.CMBD, StrikeOuts = 90 };
    var team = new PlayerTeam
    {
      Code = "SEA",
      AlternativeCode = "SE",
      LeagueId = "AL",
      City = "Seattle",
      Nickname = "Mariners"
    };
    var position = new Position
    {
      Code = "SP",
      FullName = "Starting Pitcher",
      PlayerType = PlayerType.P,
      SortOrder = 1
    };
    var player = new Player
    {
      Id = id,
      MlbAmId = 123456,
      Type = PlayerType.P,
      FirstName = "Logan",
      LastName = "Gilbert",
      Age = 27,
      Status = PlayerStatus.NE,
      AverageDraftPick = 89.4,
      AverageDraftPickMin = 74,
      AverageDraftPickMax = 112,
      Reliability = 82.5,
      MayberryMethod = 68,
      League1 = LeagueStatus.A,
      League2 = LeagueStatus.R,
      BattingStats = [battingStats],
      PitchingStats = [pitchingStats],
      Team = team,
      Positions = [position]
    };
    Assert.Equal(id, player.Id);
    Assert.Equal(123456, player.MlbAmId);
    Assert.Equal(PlayerType.P, player.Type);
    Assert.Equal("Logan", player.FirstName);
    Assert.Equal("Gilbert", player.LastName);
    Assert.Equal(27, player.Age);
    Assert.Equal(PlayerStatus.NE, player.Status);
    Assert.Equal(89.4, player.AverageDraftPick);
    Assert.Equal(74, player.AverageDraftPickMin);
    Assert.Equal(112, player.AverageDraftPickMax);
    Assert.Equal(82.5, player.Reliability);
    Assert.Equal(68, player.MayberryMethod);
    Assert.Equal(LeagueStatus.A, player.League1);
    Assert.Equal(LeagueStatus.R, player.League2);
    Assert.Single(player.BattingStats);
    Assert.Equal(50, player.BattingStats[0].Hits);
    Assert.Single(player.PitchingStats);
    Assert.Equal(90, player.PitchingStats[0].StrikeOuts);
    Assert.Equal("SEA", player.Team.Code);
    Assert.Single(player.Positions);
    Assert.Equal("SP", player.Positions[0].Code);
  }
}