using System.Collections.Generic;
using FantasyBaseball.Common.Enums;
using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.Common.UnitTests.Models;

public class PositionTests
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

    [Theory]
    [InlineData(PlayerType.B)]
    [InlineData(PlayerType.P)]
    public void SupportsAllPlayerTypes(PlayerType playerType)
    {
        var position = new Position { PlayerType = playerType };
        Assert.Equal(playerType, position.PlayerType);
    }

    [Fact]
    public void CanAddMultipleAdditionalPositions()
    {
        var add1 = new Position { Code = "DH", FullName = "Designated Hitter", PlayerType = PlayerType.B, SortOrder = 12 };
        var add2 = new Position { Code = "OF", FullName = "Outfield", PlayerType = PlayerType.B, SortOrder = 7 };
        var add3 = new Position { Code = "IF", FullName = "Infield", PlayerType = PlayerType.B, SortOrder = 6 };
        var position = new Position
        {
            Code = "1B",
            FullName = "First Base",
            PlayerType = PlayerType.B,
            SortOrder = 4,
            AdditionalPositions = [add1, add2, add3]
        };
        Assert.Equal(3, position.AdditionalPositions.Count);
        Assert.Equal("DH", position.AdditionalPositions[0].Code);
        Assert.Equal("OF", position.AdditionalPositions[1].Code);
        Assert.Equal("IF", position.AdditionalPositions[2].Code);
    }

    [Fact]
    public void SetsPitcherPosition()
    {
        var position = new Position
        {
            Code = "SP",
            FullName = "Starting Pitcher",
            PlayerType = PlayerType.P,
            SortOrder = 1
        };
        Assert.Equal("SP", position.Code);
        Assert.Equal("Starting Pitcher", position.FullName);
        Assert.Equal(PlayerType.P, position.PlayerType);
        Assert.Equal(1, position.SortOrder);
    }

    [Fact]
    public void SetsNegativeSortOrder()
    {
        var position = new Position { SortOrder = -1 };
        Assert.Equal(-1, position.SortOrder);
    }

    [Fact]
    public void SetsMaxIntSortOrder()
    {
        var position = new Position { SortOrder = int.MaxValue };
        Assert.Equal(int.MaxValue, position.SortOrder);
    }

    [Fact]
    public void SetsEmptyCode()
    {
        var position = new Position { Code = "" };
        Assert.Empty(position.Code);
    }

    [Fact]
    public void SetsEmptyFullName()
    {
        var position = new Position { FullName = "" };
        Assert.Empty(position.FullName);
    }

    [Fact]
    public void SetsNullAdditionalPositions()
    {
        var position = new Position { AdditionalPositions = null! };
        Assert.Null(position.AdditionalPositions);
    }
}
