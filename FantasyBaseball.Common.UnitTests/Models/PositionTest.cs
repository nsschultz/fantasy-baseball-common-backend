using System.Collections.Generic;
using FantasyBaseball.Common.Enums;
using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Models;

public class PositionTest
{
  [Fact]
  public void HasEmptyAdditionalPositionsByDefault() => Assert.Empty(new Position().AdditionalPositions);

  [Fact]
  public void SetsAllProperties()
  {
    var additionalPosition = new Position
    {
      Code = "DH",
      FullName = "Designated Hitter",
      PlayerType = PlayerType.B,
      SortOrder = 12
    };

    var position = new Position
    {
      Code = "1B",
      FullName = "First Base",
      PlayerType = PlayerType.B,
      SortOrder = 4,
      AdditionalPositions = new List<Position> { additionalPosition }
    };

    Assert.Equal("1B", position.Code);
    Assert.Equal("First Base", position.FullName);
    Assert.Equal(PlayerType.B, position.PlayerType);
    Assert.Equal(4, position.SortOrder);
    Assert.Single(position.AdditionalPositions);
    Assert.Equal("DH", position.AdditionalPositions[0].Code);
  }
}
