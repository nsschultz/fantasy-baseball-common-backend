using System.Text.Json.Serialization;

namespace FantasyBaseball.Common.Models;

/// <summary>Info for a given baseball team.</summary>
public class PlayerTeam
{
  /// <summary>The team's main code.</summary>
  [JsonRequired] public string Code { get; set; }

  /// <summary>The team's alternative code.</summary>
  public string AlternativeCode { get; set; }

  /// <summary>The league a team belongs to.</summary>
  [JsonRequired] public string LeagueId { get; set; }

  /// <summary>The team's city.</summary>
  [JsonRequired] public string City { get; set; }

  /// <summary>The team's nickname.</summary>
  [JsonRequired] public string Nickname { get; set; }
}