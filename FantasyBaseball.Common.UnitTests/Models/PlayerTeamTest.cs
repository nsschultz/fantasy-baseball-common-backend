using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Models;

public class PlayerTeamTest
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
}
