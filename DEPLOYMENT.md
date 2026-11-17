# Deployment Guide

## Overview

This guide describes the process for releasing new versions of the fantasy-baseball-common-backend library and deploying to distribution channels (NuGet and Docker Hub).

## Release Process

### Step 1: Prepare Release Branch

Before releasing, ensure the main branch is ready:

```bash
# 1. Update main branch
git checkout main
git pull origin main

# 2. Verify all checks pass
# - GitHub Actions workflows all green
# - SonarCloud quality gate passing
# - No critical security vulnerabilities
```

### Step 2: Version Bumping

Update version files according to [Semantic Versioning](https://semver.org/):

```bash
# 1. Determine version bump (MAJOR.MINOR.PATCH)
# - MAJOR: Breaking changes (e.g., 1.0.0 → 2.0.0)
# - MINOR: New features, backward compatible (e.g., 1.0.0 → 1.1.0)
# - PATCH: Bug fixes (e.g., 1.0.0 → 1.0.1)

# 2. Update version files
echo "X.Y.Z" > version-library.txt  # NuGet version
echo "X.Y.Z" > version-image.txt    # Docker version

# Note: Versions should be kept in sync for releases
# Only separate them if Docker needs a quick rebuild
```

### Step 3: Update CHANGELOG

Update CHANGELOG.md with release notes:

```markdown
## [X.Y.Z] - YYYY-MM-DD

### Added
- New features (if MINOR or MAJOR)

### Changed
- Changes to existing features

### Fixed
- Bug fixes (if PATCH)

### Security
- Security fixes

### Breaking Changes
- Breaking changes (if MAJOR)
- Migration guide for consumers

---

## [Previous Version]
...
```

### Step 4: Commit and Push

```bash
# 1. Stage changes
git add version-library.txt version-image.txt CHANGELOG.md

# 2. Commit with clear message
git commit -m "Release v$(cat version-library.txt)

- Added: [feature descriptions if any]
- Fixed: [bug fixes if any]
- Breaking changes: [if applicable]

See CHANGELOG.md for full details."

# 3. Push to main
git push origin main

# 4. Wait for CI/CD to complete
# - Builds should trigger automatically
# - Monitor GitHub Actions workflow
```

### Step 5: Create Git Tag and Release

After CI/CD completes successfully:

```bash
# 1. Create Git tag
VERSION=$(cat version-library.txt)
git tag -a "v${VERSION}" -m "Release v${VERSION}"
git push origin "v${VERSION}"

# 2. Create GitHub Release
# Go to: https://github.com/nsschultz/fantasy-baseball-common-backend/releases
# - Click "Create a new release"
# - Select tag: v{VERSION}
# - Title: Release v{VERSION}
# - Description: Copy from CHANGELOG.md
# - Publish release
```

## Verification Checklist

After release, verify all artifacts are available:

### NuGet Package Verification (5-10 minutes)

```bash
# 1. Check GitHub Packages
# URL: https://github.com/nsschultz/fantasy-baseball-common-backend/packages
# Verify:
# - Package shows latest version
# - Package version matches version-library.txt
# - Creation timestamp is recent (within 5-10 minutes)

# 2. Test package installation
dotnet package search fantasy-baseball-common-backend \
  --source "https://nuget.pkg.github.com/nsschultz/index.json"

# 3. Verify package contents
dotnet nuget search fantasy-baseball-common-backend
```

### Docker Image Verification (5-10 minutes)

```bash
# 1. Check Docker Hub
# URL: https://hub.docker.com/r/nschultz/fantasy-baseball-common-backend
# Verify:
# - Image shows latest version
# - Tags present:
#   - {VERSION} (e.g., 1.1.0)
#   - latest
#   - arch tags (amd64, arm64) - if applicable
# - Created timestamp is recent

# 2. Check platform availability
docker pull nschultz/fantasy-baseball-common-backend:{VERSION}
# Should work for both linux/amd64 and linux/arm64

# 3. Verify image metadata
docker inspect nschultz/fantasy-baseball-common-backend:{VERSION}
```

### SonarCloud Verification

```bash
# Check: https://sonarcloud.io/project/overview?id=nsschultz_fantasy-baseball-common-backend
# Verify:
# - Latest version shows in project
# - Quality gate is passing
# - Coverage meets 80% target (or documented threshold)
# - No new security vulnerabilities
```

### GitHub Release Verification

```bash
# Check: https://github.com/nsschultz/fantasy-baseball-common-backend/releases
# Verify:
# - Release tag matches version (v{VERSION})
# - Release notes are populated
# - GitHub Packages asset is available (if configured)
```

## Release Quality Gates

**All of the following MUST be satisfied before release**:

- ✅ All tests passing (0 failures)
- ✅ Code coverage ≥ 80% (SonarCloud)
- ✅ SonarCloud quality gate: PASSING
- ✅ No CRITICAL vulnerabilities (Trivy/SonarQube)
- ✅ CHANGELOG.md updated with release notes
- ✅ Breaking changes documented (if MAJOR version bump)
- ✅ Version files in sync (library and image)
- ✅ Git tag created with format: v{VERSION}
- ✅ GitHub release created with notes

## Rollback Procedures

### Rollback NuGet Package

If a critical bug is found in released version:

```bash
# 1. Create hotfix branch
git checkout -b hotfix/v{VERSION+1}

# 2. Fix the issue
# ... make your fixes ...

# 3. Bump patch version
CURRENT=$(cat version-library.txt)
MAJOR=$(echo $CURRENT | cut -d. -f1)
MINOR=$(echo $CURRENT | cut -d. -f2)
PATCH=$(echo $CURRENT | cut -d. -f3)
NEW_PATCH=$((PATCH + 1))
echo "${MAJOR}.${MINOR}.${NEW_PATCH}" > version-library.txt

# 4. Update CHANGELOG
# Add new entry for patch release

# 5. Commit and release
git add version-library.txt CHANGELOG.md
git commit -m "Hotfix v${MAJOR}.${MINOR}.${NEW_PATCH}: [description]"
git push origin main

# 6. Create tag and release as normal
```

**For Consumers**:
- If using version pinning: Update to new patch version
- If using version range: Will automatically get patch update

### Rollback Docker Image

To rollback Docker image to previous version:

```bash
# 1. Get previous version tag
docker pull nschultz/fantasy-baseball-common-backend:1.0.0  # previous working version

# 2. Retag as latest
docker tag nschultz/fantasy-baseball-common-backend:1.0.0 \
           nschultz/fantasy-baseball-common-backend:latest

# 3. Push retag
docker push nschultz/fantasy-baseball-common-backend:latest

# Note: This is a temporary measure while hotfix is being prepared
```

**For Consumers**:
- If using `latest` tag: Automatically get rolled-back version
- If using version pin: Must manually pull previous version tag

## Hotfix Release (Emergency)

For critical bugs discovered in production:

```bash
# 1. Create urgent hotfix branch
git checkout -b hotfix/critical-issue-v{VERSION+1}

# 2. Fix issue and add test coverage
dotnet test  # Must pass all tests

# 3. Bump PATCH version only
echo "$(cat version-library.txt | awk -F. '{print $1"."$2"."($3+1)}' )" > version-library.txt
echo "$(cat version-image.txt | awk -F. '{print $1"."$2"."($3+1)}' )" > version-image.txt

# 4. Update CHANGELOG with HOTFIX label
# [X.Y.Z-hotfix] - YYYY-MM-DD
# ### Security/Fixed
# - Critical fix description

# 5. Commit, push, and release immediately
git add .
git commit -m "HOTFIX v$(cat version-library.txt): Description"
git push origin main

# 6. Monitor CI/CD closely
# - Verify workflows complete successfully
# - Verify artifacts appear in distribution channels
# - Notify consumers of critical fix

# 7. Post-release
# - Document root cause
# - Plan process improvement to prevent recurrence
# - Create incident post-mortem
```

## Automated CI/CD Workflow

The GitHub Actions workflows automate validation and publishing:

### On Pull Requests

1. **pr-validation.yml** runs:
   - Runs dotnet build
   - Runs dotnet test with coverage
   - Publishes coverage to SonarCloud
   - Quality gate check

### On Push to Main

1. **nuget-package.yml** triggers:
   - Reads version from version-library.txt
   - Packs FantasyBaseball.Common
   - Publishes to GitHub Packages
   - Automatically versioned

2. **docker-image.yml** triggers:
   - Reads version from version-image.txt
   - Builds multi-platform Docker images (amd64, arm64)
   - Pushes to Docker Hub
   - Tags with version, latest, and platform tags

### Manual Steps Required

Only manual Git operations are needed:
- Version file updates
- CHANGELOG updates
- Git commit and tag creation
- GitHub release creation

## Success Metrics

After a successful release:

| Metric | Target | How to Verify |
|--------|--------|---------------|
| Build Success | 100% | GitHub Actions all green |
| Package Availability | 5-10 min | GitHub Packages shows version |
| Image Availability | 5-10 min | Docker Hub shows tags |
| Quality Gate | Passing | SonarCloud dashboard |
| Coverage | ≥80% | SonarCloud coverage report |
| Security | 0 Critical | Trivy scan results |
| Test Results | 100% pass | GitHub Actions test output |

## Troubleshooting

### NuGet Package Not Published

```bash
# 1. Check GitHub Actions log
# URL: https://github.com/nsschultz/fantasy-baseball-common-backend/actions

# 2. Verify secrets are configured
# - GITHUB_TOKEN: Must have write:packages permission

# 3. Check version format
# - Should be X.Y.Z format
# - No "v" prefix

# 4. Verify package name matches csproj
cat FantasyBaseball.Common/FantasyBaseball.Common.csproj | grep PackageId
```

### Docker Image Not Published

```bash
# 1. Check GitHub Actions log
# - Look for login/push errors

# 2. Verify Docker Hub secrets
# - DOCKERHUB_USERNAME: Your Docker Hub username
# - DOCKERHUB_TOKEN: Personal access token (not password)

# 3. Check image naming
# - Should be: nschultz/fantasy-baseball-common-backend:VERSION
# - Registry format correct for Docker Hub

# 4. Verify multi-platform build
# - Should build for linux/amd64,linux/arm64
# - Check QEMU setup in workflow
```

### SonarCloud Analysis Fails

```bash
# 1. Check SONAR_TOKEN secret
# - Must be configured in GitHub secrets
# - Token should be valid and have correct permissions

# 2. Check scan.sh
# - Verify version is passed correctly
# - Verify project key format

# 3. Run locally to debug
cd /workspaces/fantasy-baseball-common-backend
docker compose -f .docker-compose/docker-compose-ci.yaml up --build
```

## Release Communication

After successful release:

1. **Update README.md** if version bump is significant
2. **Notify consumers**:
   - Email: relevant teams/stakeholders
   - Slack: #releases channel (if applicable)
   - GitHub: Release notes mention breaking changes
3. **Document in team wiki** (if applicable)
4. **Update dependency docs** for consumers

## Related Documentation

- **[SLO_SLA.md](./SLO_SLA.md)** - Service level targets
- **[DEVOPS_RUNBOOK.md](./DEVOPS_RUNBOOK.md)** - Operational procedures
- **[CHANGELOG.md](./CHANGELOG.md)** - Release history
- **[README.md](./README.md)** - Project overview

---

**Last Updated**: 2026-11-15  
**Maintained By**: Development Team  
**Questions?** See DEVOPS_RUNBOOK.md or SLO_SLA.md
