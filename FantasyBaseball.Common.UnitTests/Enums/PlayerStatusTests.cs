using FantasyBaseball.Common.Enums;
using Xunit;
using static FantasyBaseball.Common.Utility.EnumHelper;

namespace FantasyBaseball.Common.UnitTests.Enums;

public class PlayerStatusTests
{
    [Theory]
    [InlineData(PlayerStatus.XX, "")]
    [InlineData(PlayerStatus.IL, "Injured List")]
    [InlineData(PlayerStatus.NA, "Not Available")]
    [InlineData(PlayerStatus.NE, "New Entry")]
    public void GetDescription_WithEnumValue_ReturnsCorrectDescription(PlayerStatus type, string description) => Assert.Equal(description, GetDescription(type));

    [Theory]
    [InlineData(PlayerStatus.XX, "")]
    [InlineData(PlayerStatus.IL, "INJURED List")]
    [InlineData(PlayerStatus.NA, "NOT Available")]
    [InlineData(PlayerStatus.NE, "New ENTRY")]
    [InlineData(PlayerStatus.XX, "Normal")]
    [InlineData(PlayerStatus.XX, null)]
    public void GetFromDescription_WithDescription_ReturnsCorrectEnum(PlayerStatus type, string? desc) => Assert.Equal(type, GetFromDescription<PlayerStatus>(desc));

    [Theory]
    [InlineData(PlayerStatus.XX, "")]
    [InlineData(PlayerStatus.IL, "il")]
    [InlineData(PlayerStatus.NA, "nA")]
    [InlineData(PlayerStatus.NE, "Ne")]
    [InlineData(PlayerStatus.XX, "XX")]
    [InlineData(PlayerStatus.XX, null)]
    public void GetFromKey_WithKey_ReturnsCorrectEnum(PlayerStatus type, string? key) => Assert.Equal(type, GetFromKey<PlayerStatus>(key));
}