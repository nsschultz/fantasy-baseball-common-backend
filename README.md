[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nsschultz_fantasy-baseball-common-backend&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=nsschultz_fantasy-baseball-common-backend)

## Common Backend

Common code shared between the fantasy baseball backend services.

### Project Overview

This project provides shared libraries and utilities for the fantasy baseball backend services. It includes:

- **Models**: Core data models for players, teams, positions, batting stats, and pitching stats
- **Enums**: Standardized enumerations for player types, statuses, league statuses, and stats types
- **Exceptions**: Custom exception types with built-in HTTP error handling via `ExceptionFilter`
- **Utilities**: Helper methods for enum operations and mathematical calculations

### Quick Start

#### Prerequisites

- .NET 10 SDK or later
- Docker (for containerized builds)

#### Building Locally

```bash
# Build the project
dotnet build

# Run unit tests
dotnet test

# Build the library
dotnet publish -c Release
```

### Code Quality

This project includes:
- Unit tests with xUnit framework (145+ test cases)
- XML documentation comments on all public APIs
- SonarQube analysis integration

### Architecture

**Target Framework**: .NET 10.0
**Nullable Reference Types**: Enabled
**Language Features**: C# 14

The common library is structured as a shared class library providing:
1. Data models used across backend services
2. Reusable exception handling middleware
3. Utility functions for enums and mathematical operations

### Build Image

```bash
version=$(cat version-image.txt) && docker build -t nschultz/fantasy-baseball-common-backend:$version .
```

### Dev Container

- In VS Code, use the "Clone Repository into Container Volume..." option to open the workspace.
- Tasks are setup in [tasks.json](.vscode/tasks.json).

### Documentation

- See [.github/agents/CSharpExpert.agent.md](.github/agents/CSharpExpert.agent.md) for C# development guidelines
- All public APIs include XML documentation comments (`/// <summary>`, `/// <param>`, `/// <returns>`)

### Testing

The test suite covers:
- Model property initialization and default values
- Enum value support across all player and stats types
- Edge cases: empty strings, null references, boundary values
- Collection handling for multi-item scenarios

Run tests with:
```bash
# Run all tests (all test projects)
dotnet test

# Run specific test project
dotnet test FantasyBaseball.Common.UnitTests/

# Run with coverage
dotnet test /p:CollectCoverage=true
```

**Multi-Project Testing**: The build process automatically discovers and runs all test projects matching the naming patterns `*Tests.csproj` or `*Test.csproj`, collecting and merging coverage reports for each.

### Contributing

Follow the C# conventions documented in [.github/agents/CSharpExpert.agent.md](.github/agents/CSharpExpert.agent.md):
- Use nullable reference types
- Add XML documentation for public APIs
- Follow SOLID principles and modern C# patterns
- Write focused unit tests (xUnit framework)
- Run tests before committing changes

