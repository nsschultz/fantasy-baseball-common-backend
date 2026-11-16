## Description

Briefly describe the changes in this PR.

## Type of Change

- [ ] Bug fix (non-breaking change fixing an issue)
- [ ] New feature (non-breaking change adding functionality)
- [ ] Breaking change (fix or feature causing existing functionality to change)
- [ ] Documentation update

## Related Issues

Fixes #(issue number)

## Testing

Describe how you tested these changes:
- [ ] Added unit tests
- [ ] Updated existing tests
- [ ] Manually tested locally

## Code Quality Checklist

- [ ] My code follows the project's C# conventions (see .github/agents/CSharpExpert.agent.md)
- [ ] I have added XML documentation comments to public APIs
- [ ] I have added/updated unit tests
- [ ] All tests pass locally (`dotnet test`)
- [ ] Code builds successfully (`dotnet build`)
- [ ] Code formatting is correct (`dotnet format --verify-no-changes`)
- [ ] No new compiler warnings
- [ ] Nullable reference types are properly handled
- [ ] No hardcoded secrets or credentials

## Coverage

- [ ] Code coverage maintained at ≥80%
- [ ] New code is covered by tests

## Documentation

- [ ] Updated CHANGELOG.md (if applicable)
- [ ] Updated README.md (if user-facing changes)
- [ ] Added/updated XML documentation
- [ ] Updated SLO/SLA documentation (if service level changes)

## Breaking Changes

- [ ] This PR includes breaking changes

If yes, describe the impact and migration path for consumers:

## Checklist

- [ ] I have reviewed my own changes
- [ ] I have run tests locally and they all pass
- [ ] I have run `dotnet format` and code is properly formatted
- [ ] I have not committed any secrets or credentials
- [ ] The PR title clearly describes the change
- [ ] Related issues are linked

## Additional Context

Add any other context about the PR here.

---

**Note**: This PR will trigger automated checks:
- Build validation
- Unit tests
- Code quality scan (SonarCloud)
- Security scanning (Trivy)
- Code formatting check

All checks must pass before merging.
