using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Models;

public class PlayerTeamTests
{
  [Fact]
  public void SetsAllProperties()
  {
    var playerTeam = new PlayerTeam
    {
      Code = "NYY",
      AlternativeCode = "NY",
      LeagueId = "AL",
      City = "New York",
      Nickname = "Yankees"
    };
    Assert.Equal("NYY", playerTeam.Code);
    Assert.Equal("NY", playerTeam.AlternativeCode);
    Assert.Equal("AL", playerTeam.LeagueId);
    Assert.Equal("New York", playerTeam.City);
    Assert.Equal("Yankees", playerTeam.Nickname);
  }

  [Fact]
  public void SetsNullAlternativeCode()
  {
    var playerTeam = new PlayerTeam { AlternativeCode = null };
    Assert.Null(playerTeam.AlternativeCode);
  }

  [Fact]
  public void SetsEmptyAlternativeCode()
  {
    var playerTeam = new PlayerTeam { AlternativeCode = "" };
    Assert.Empty(playerTeam.AlternativeCode);
  }

  [Fact]
  public void SetsEmptyCode()
  {
    var playerTeam = new PlayerTeam { Code = "" };
    Assert.Empty(playerTeam.Code);
  }

  [Fact]
  public void SetsEmptyLeagueId()
  {
    var playerTeam = new PlayerTeam { LeagueId = "" };
    Assert.Empty(playerTeam.LeagueId);
  }

  [Fact]
  public void SetsEmptyCity()
  {
    var playerTeam = new PlayerTeam { City = "" };
    Assert.Empty(playerTeam.City);
  }

  [Fact]
  public void SetsEmptyNickname()
  {
    var playerTeam = new PlayerTeam { Nickname = "" };
    Assert.Empty(playerTeam.Nickname);
  }

  [Fact]
  public void SetsLongValues()
  {
    var longString = new string('a', 1000);
    var playerTeam = new PlayerTeam
    {
      Code = longString,
      City = longString,
      Nickname = longString
    };
    Assert.Equal(longString, playerTeam.Code);
    Assert.Equal(longString, playerTeam.City);
    Assert.Equal(longString, playerTeam.Nickname);
  }
}
