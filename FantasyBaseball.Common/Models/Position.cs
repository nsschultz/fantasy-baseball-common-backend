using System.Collections.Generic;
using System.Text.Json.Serialization;
using FantasyBaseball.Common.Enums;

namespace FantasyBaseball.Common.Models;

/// <summary>All of the information that makes up a baseball position.</summary>
public class Position
{
  /// <summary>The position's code.</summary>
  [JsonRequired] public string Code { get; set; }

  /// <summary>The full name of the position.</summary>
  [JsonRequired] public string FullName { get; set; }

  /// <summary>The type of player (batter or pitcher) that plays this position.</summary>
  [JsonRequired] public PlayerType PlayerType { get; set; }

  /// <summary>The order this position should be sorted in.</summary>
  [JsonRequired] public int SortOrder { get; set; }

  /// <summary>Additional positions that this position is eligible for.</summary>
  [JsonRequired] public List<Position> AdditionalPositions { get; set; } = [];
}