# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased

### Added
- Multi-project test discovery and coverage gathering in scan.sh
  - Automatically discovers test projects matching *Tests.csproj or *Test.csproj patterns
  - Runs tests for each project with individual coverage collection
  - Merges all OpenCover reports into a unified report for SonarCloud
  - Each test project generates its own .trx result file for full traceability

### Changed
- Hardened GitHub Actions workflows with concurrency controls and job timeouts
- Upgraded GitHub Actions workflow dependencies to Node.js 24-compatible major versions (replacing Node.js 20-based action versions), including `docker/setup-qemu-action`, `docker/login-action`, and `docker/build-push-action`
- Updated DevOps documentation to align with current workflow names and CI/CD behavior
- Updated DevOps agent documentation to remove explicit Node.js 20 references
- Enhanced scan.sh with comprehensive documentation header and improved error handling
- Updated `scan.sh` coverage merge path to use `../coverage-results/coverage.json` for multi-project coverage aggregation
- Bumped Docker image version to `1.2.1` in `version-image.txt`

### Deprecated
- Features that will be removed soon

### Removed
- Removed features

### Fixed
- Improved nullability annotations in `EnumHelper` to match runtime behavior for null enum lookup inputs
- Removed nullable reference warnings from unit tests

### Security
- Upgraded `Microsoft.OpenApi` to `3.11.1` to remove known high-severity vulnerabilities

---

## [1.1.0] - 2026-11-15

### Added
- Initial production release
- Comprehensive test suite (145+ test cases)
- SonarCloud integration for code quality
- Multi-platform Docker builds (amd64, arm64)
- NuGet package distribution via GitHub Packages
- Docker image distribution via Docker Hub
- DevOps documentation (DEPLOYMENT.md, DEVOPS_RUNBOOK.md, SLO_SLA.md)

### Changed
- Updated to .NET 10.0
- Improved code structure and organization
- Enhanced exception handling with ExceptionFilter

### Security
- Implemented SonarQube SAST scanning
- Added Trivy container scanning
- Enabled dependency vulnerability scanning

---

## Release Template

Use this template for each new release:

```markdown
## [X.Y.Z] - YYYY-MM-DD

### Added
- Feature 1 description
- Feature 2 description

### Changed
- Change 1 description

### Deprecated
- Deprecated feature description (if applicable)

### Removed
- Removed feature description (if applicable)

### Fixed
- Bug fix 1 description
- Bug fix 2 description

### Security
- Security fix description (if applicable)

### Breaking Changes
- Description of breaking change
- Migration guide for consumers

### Contributors
- @username1
- @username2
```

---

## Version History

### Versioning Strategy

This project follows [Semantic Versioning](https://semver.org/):

- **MAJOR version** (X.0.0): Incompatible API changes
- **MINOR version** (0.X.0): Backward-compatible functionality additions
- **PATCH version** (0.0.X): Backward-compatible bug fixes

### Release Process

1. Update version in `version-library.txt` and `version-image.txt`
2. Update CHANGELOG.md with release notes
3. Commit: `git commit -m "Release vX.Y.Z"`
4. Tag: `git tag -a "vX.Y.Z" -m "Release vX.Y.Z"`
5. Push: `git push origin main && git push origin vX.Y.Z`
6. Create GitHub Release with notes from CHANGELOG.md

See [DEPLOYMENT.md](./DEPLOYMENT.md) for complete release procedures.

---

## How to Read This Changelog

- **[Unreleased]**: Changes in development, not yet released
- **[Version]**: Released version with date
- **Added**: New features
- **Changed**: Changes to existing features
- **Deprecated**: Features marked for future removal
- **Removed**: Deleted features
- **Fixed**: Bug fixes
- **Security**: Security vulnerability fixes
- **Breaking Changes**: Incompatible changes requiring consumer action

---

## For Consumers

When upgrading to a new version:

1. Check this changelog for your version
2. Review **Added** features (new capabilities)
3. Review **Changed** section (behavior changes)
4. Review **Breaking Changes** section (required actions)
5. Review **Security** fixes (important updates)

### Latest Package Release: [1.1.0]

Current stable NuGet version: **1.1.0**  
Current stable Docker image version: **1.1.3**

Upgrade with:
```bash
dotnet add package fantasy-baseball-common-backend --version 1.1.0
```

Or with Docker:
```bash
docker pull nschultz/fantasy-baseball-common-backend:1.1.0
```

---

## Support

For issues with a specific version:
- Check [GitHub Issues](https://github.com/nsschultz/fantasy-baseball-common-backend/issues)
- Review [DEPLOYMENT.md](./DEPLOYMENT.md) for upgrade guidance
- Contact development team

## Related Documentation

- [DEPLOYMENT.md](./DEPLOYMENT.md) - Release procedures
- [DEVOPS_RUNBOOK.md](./DEVOPS_RUNBOOK.md) - Operational guide
- [SLO_SLA.md](./SLO_SLA.md) - Service level objectives
- [README.md](./README.md) - Project overview

---

**Last Updated**: 2026-11-18
