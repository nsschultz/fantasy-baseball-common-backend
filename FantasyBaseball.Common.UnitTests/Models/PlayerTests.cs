using System;
using FantasyBaseball.Common.Enums;
using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Models;

public class PlayerTests
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
  public void HasDefaultIdValue()
  {
    var player = new Player();
    Assert.Equal(Guid.Empty, player.Id);
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

  [Theory]
  [InlineData(PlayerType.B)]
  [InlineData(PlayerType.P)]
  public void SupportsAllPlayerTypes(PlayerType playerType)
  {
    var player = new Player { Type = playerType };
    Assert.Equal(playerType, player.Type);
  }

  [Theory]
  [InlineData(PlayerStatus.XX)]
  [InlineData(PlayerStatus.IL)]
  [InlineData(PlayerStatus.NA)]
  [InlineData(PlayerStatus.NE)]
  public void SupportsAllPlayerStatuses(PlayerStatus status)
  {
    var player = new Player { Status = status };
    Assert.Equal(status, player.Status);
  }

  [Fact]
  public void CanAddMultipleBattingStats()
  {
    var stats1 = new BattingStats { StatsType = StatsType.YTD, Hits = 100 };
    var stats2 = new BattingStats { StatsType = StatsType.PROJ, Hits = 50 };
    var stats3 = new BattingStats { StatsType = StatsType.CMBD, Hits = 150 };
    var player = new Player { BattingStats = [stats1, stats2, stats3] };
    Assert.Equal(3, player.BattingStats.Count);
    Assert.Equal(StatsType.YTD, player.BattingStats[0].StatsType);
    Assert.Equal(StatsType.PROJ, player.BattingStats[1].StatsType);
    Assert.Equal(StatsType.CMBD, player.BattingStats[2].StatsType);
  }

  [Fact]
  public void CanAddMultiplePitchingStats()
  {
    var stats1 = new PitchingStats { StatsType = StatsType.YTD, Wins = 10 };
    var stats2 = new PitchingStats { StatsType = StatsType.PROJ, Wins = 12 };
    var stats3 = new PitchingStats { StatsType = StatsType.CMBD, Wins = 22 };
    var player = new Player { PitchingStats = [stats1, stats2, stats3] };
    Assert.Equal(3, player.PitchingStats.Count);
    Assert.Equal(StatsType.YTD, player.PitchingStats[0].StatsType);
    Assert.Equal(StatsType.PROJ, player.PitchingStats[1].StatsType);
    Assert.Equal(StatsType.CMBD, player.PitchingStats[2].StatsType);
  }

  [Fact]
  public void CanAddMultiplePositions()
  {
    var pos1 = new Position { Code = "C", FullName = "Catcher", PlayerType = PlayerType.B, SortOrder = 1 };
    var pos2 = new Position { Code = "1B", FullName = "First Base", PlayerType = PlayerType.B, SortOrder = 2 };
    var pos3 = new Position { Code = "DH", FullName = "Designated Hitter", PlayerType = PlayerType.B, SortOrder = 3 };
    var player = new Player { Positions = [pos1, pos2, pos3] };
    Assert.Equal(3, player.Positions.Count);
    Assert.Equal("C", player.Positions[0].Code);
    Assert.Equal("1B", player.Positions[1].Code);
    Assert.Equal("DH", player.Positions[2].Code);
  }

  [Fact]
  public void SetsNegativeAge()
  {
    var player = new Player { Age = -5 };
    Assert.Equal(-5, player.Age);
  }

  [Fact]
  public void SetsMaxIntMlbAmId()
  {
    var player = new Player { MlbAmId = int.MaxValue };
    Assert.Equal(int.MaxValue, player.MlbAmId);
  }

  [Fact]
  public void SetsEmptyNames()
  {
    var player = new Player { FirstName = "", LastName = "" };
    Assert.Empty(player.FirstName);
    Assert.Empty(player.LastName);
  }

  [Fact]
  public void SetsNullTeam()
  {
    var player = new Player { Team = null };
    Assert.Null(player.Team);
  }
}