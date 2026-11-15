using FantasyBaseball.Common.Enums;
using Xunit;
using static FantasyBaseball.Common.Utility.EnumHelper;

namespace FantasyBaseball.Common.UnitTests.Enums;

public class PlayerTypeTests
{
  [Theory]
  [InlineData(PlayerType.U, "Unknown")]
  [InlineData(PlayerType.B, "Batter")]
  [InlineData(PlayerType.P, "Pitcher")]
  public void GetDescription_WithEnumValue_ReturnsCorrectDescription(PlayerType type, string description) => Assert.Equal(description, GetDescription(type));

  [Theory]
  [InlineData(PlayerType.U, "Unknown")]
  [InlineData(PlayerType.B, "batter")]
  [InlineData(PlayerType.P, "PITCHER")]
  [InlineData(PlayerType.U, "")]
  [InlineData(PlayerType.U, null)]
  public void GetFromDescription_WithDescription_ReturnsCorrectEnum(PlayerType type, string desc) => Assert.Equal(type, GetFromDescription<PlayerType>(desc));

  [Theory]
  [InlineData(PlayerType.U, "U")]
  [InlineData(PlayerType.B, "b")]
  [InlineData(PlayerType.P, "P")]
  [InlineData(PlayerType.U, "")]
  [InlineData(PlayerType.U, null)]
  public void GetFromKey_WithKey_ReturnsCorrectEnum(PlayerType type, string key) => Assert.Equal(type, GetFromKey<PlayerType>(key));
}