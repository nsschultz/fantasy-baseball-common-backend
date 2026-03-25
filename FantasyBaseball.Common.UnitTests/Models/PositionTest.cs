using FantasyBaseball.Model.Enums;
using Xunit;

namespace FantasyBaseball.Model.UnitTests;

public class PositionTest
{
  [Fact]
  public void DefaultsSetTest()
  {
    var obj = new Position();
    Assert.Null(obj.Code);
    Assert.Null(obj.FullName);
    Assert.Equal(PlayerType.U, obj.PlayerType);
    Assert.Equal(0, obj.SortOrder);
    Assert.Empty(obj.AdditionalPositions);
  }
}