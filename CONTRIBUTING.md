# Contributing to Fantasy Baseball Common Backend

Thank you for contributing! This document provides guidelines for working with this shared library.

## Overview

This is a shared library project providing core models, enums, and utilities used by multiple fantasy baseball backend services. Changes here impact multiple downstream services, so quality and backward compatibility are critical.

## Development Workflow

### Prerequisites
- .NET 10 SDK or later
- Git
- VS Code with the C# extension (or Visual Studio)

### Setup

```bash
# Clone the repository
git clone <repository-url>
cd fantasy-baseball-common-backend

# Open in VS Code Dev Container (recommended)
# Use "Clone Repository into Container Volume..." option

# Or build locally
dotnet build
dotnet test
```

## Coding Standards

### C# Style and Conventions

Follow the guidelines in [.github/agents/CSharpExpert.agent.md](.github/agents/CSharpExpert.agent.md), including:

- **File-scoped namespaces** (`namespace FantasyBaseball.Common.Models;`)
- **Modern C# features** (switch expressions, ranges, records for DTOs, etc.)
- **Nullable reference types** enabled (`<Nullable>enable</Nullable>`)
- **XML documentation** on all public APIs

### Documentation Requirements

**All public types and members must have XML documentation comments:**

```csharp
/// <summary>Brief description of what this does.</summary>
/// <param name="paramName">Description of the parameter.</param>
/// <returns>Description of the return value.</returns>
public string MyMethod(string paramName) { ... }
```

### Naming Conventions

- **Classes**: PascalCase (e.g., `BattingStats`, `PlayerTeam`)
- **Methods**: PascalCase (e.g., `GetDescription`, `SetValue`)
- **Async methods**: End with `Async` (e.g., `GetPlayerAsync`)
- **Properties**: PascalCase (e.g., `MlbAmId`, `FirstName`)
- **Private fields**: Lowercase with underscore (e.g., `_epsilon`, `_field`)

### Architecture

The project is organized by concern:

```
FantasyBaseball.Common/
├── Models/           - Data models (Player, Position, BattingStats, PitchingStats, PlayerTeam)
├── Enums/            - Standard enumerations (PlayerType, PlayerStatus, LeagueStatus, StatsType)
├── Exceptions/       - Custom exception types (BadRequestException, ServiceException, ExceptionFilter)
└── Utility/          - Helper methods (EnumHelper, MathHelper)
```

Keep new code in the appropriate folder. If adding a new model, place it in the `Models` folder.

## Testing

### Test Requirements

1. **Every public API change requires tests**
2. **Test file naming**: Use `ClassName + Tests.cs` suffix (e.g., `PlayerTests.cs`)
3. **Test organization**: Mirror the source structure in `FantasyBaseball.Common.UnitTests`
4. **Test framework**: xUnit (already in use)

### Writing Tests

Follow the pattern established in existing tests:

```csharp
[Fact]
public void HasEmptyCollectionsByDefault()
{
  var player = new Player();
  Assert.Empty(player.Positions);
}

[Theory]
[InlineData(PlayerType.B)]
[InlineData(PlayerType.P)]
public void SupportsAllPlayerTypes(PlayerType playerType)
{
  var player = new Player { Type = playerType };
  Assert.Equal(playerType, player.Type);
}
```

**Key principles:**
- One behavior per test method
- Use `[Fact]` for single scenarios, `[Theory]` with `[InlineData]` for multiple scenarios
- Test both happy paths and edge cases (empty strings, null values, boundary values)
- Follow Arrange-Act-Assert pattern
- Test names describe the behavior being tested

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test file
dotnet test --filter "FullyQualifiedName~PlayerTests"

# Run with coverage
dotnet tool install -g dotnet-coverage  # One-time setup
dotnet-coverage collect -f cobertura -o coverage.cobertura.xml dotnet test
```

## Common Tasks

### Adding a New Model

1. Create the class in `FantasyBaseball.Common/Models/`
2. Add XML documentation for the class and all public properties
3. Use `[JsonRequired]` for properties required during serialization
4. Initialize collections with empty list syntax: `public List<T> Items { get; set; } = [];`
5. Create corresponding test file `FantasyBaseball.Common.UnitTests/Models/YourModelTests.cs`
6. Test all properties, default values, and edge cases

Example:
```csharp
/// <summary>Information about a baseball team.</summary>
public class PlayerTeam
{
  /// <summary>The team's main code.</summary>
  [JsonRequired]
  public string Code { get; set; }

  /// <summary>The team's city.</summary>
  [JsonRequired]
  public string City { get; set; }
}
```

### Adding a New Enum

1. Create the enum in `FantasyBaseball.Common/Enums/`
2. Add `[Description]` attributes to all values
3. Document the enum with all values explained:

```csharp
/// <summary>
/// The type of player in fantasy baseball.
/// 0/U: Unknown - Type not determined
/// 1/B: Batter - Offensive player
/// 2/P: Pitcher - Defensive pitcher
/// </summary>
public enum PlayerType
{
  /// <summary>Unknown</summary>
  [Description("Unknown")] U = 0,
  
  /// <summary>Batter</summary>
  [Description("Batter")] B = 1,
  
  /// <summary>Pitcher</summary>
  [Description("Pitcher")] P = 2
}
```

### Adding a Utility Method

1. Add to `FantasyBaseball.Common/Utility/EnumHelper.cs` or `MathHelper.cs` (or create new helper)
2. Make methods `public static`
3. Add comprehensive XML documentation including parameter descriptions
4. Add unit tests covering normal cases, edge cases, and error conditions
5. Use null checks for reference types: `ArgumentNullException.ThrowIfNull(x)`

## Exception Handling

### When to Use Each Exception

- **`BadRequestException`**: Invalid input from the client (HTTP 400)
  ```csharp
  if (string.IsNullOrWhiteSpace(playerName))
    throw new BadRequestException("Player name cannot be empty.");
  ```

- **`ServiceException`**: Internal server errors (HTTP 500)
  ```csharp
  try 
  {
    // Database operation
  }
  catch (Exception ex)
  {
    throw new ServiceException("Failed to retrieve player.", ex);
  }
  ```

Both exceptions automatically map to HTTP responses via `ExceptionFilter`.

## Code Review Checklist

Before submitting a pull request, verify:

- [ ] Code compiles without warnings
- [ ] All tests pass: `dotnet test`
- [ ] New/changed public APIs have XML documentation
- [ ] Tests added for new/changed behavior
- [ ] Test file follows naming convention (`*Tests.cs`)
- [ ] No `TODO`/`FIXME` comments left
- [ ] No dead code or unused variables
- [ ] Nullable reference types properly handled
- [ ] Changes don't break backward compatibility
- [ ] README updated if needed (e.g., new features, installation steps)

## Commit Messages

Use clear, descriptive commit messages:

```
feat: add new Player model property
test: enhance model test coverage with edge cases
docs: update README with testing instructions
fix: correct documentation for ServiceException
```

Follow the [Conventional Commits](https://www.conventionalcommits.org/) format:
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation
- `test:` - Test changes
- `refactor:` - Code reorganization (no behavior change)
- `perf:` - Performance improvements

## Quality Metrics

This project maintains:
- **Code Quality**: SonarQube integration via SonarCloud
- **Test Coverage**: Minimum standards for new code
- **Documentation**: XML docs on all public APIs
- **Backward Compatibility**: Semantic versioning

## Getting Help

- Review existing code in the same folder
- Check [.github/agents/CSharpExpert.agent.md](.github/agents/CSharpExpert.agent.md) for detailed guidelines
- Look at test examples in `FantasyBaseball.Common.UnitTests`
- Review similar PRs in git history

## Version Management

The project uses semantic versioning:
- **Version file**: `version-library.txt` (for NuGet package)
- **Image version**: `version-image.txt` (for Docker)

Update version files when making releases, not on every commit.

---

Thank you for helping improve the Fantasy Baseball Common Backend!
