# DevOps Runbook

## Quick Reference for Operations

### On-Call Responsibilities

- Monitor CI/CD pipeline status
- Respond to build failures
- Respond to security alerts
- Manage emergency releases
- Track SLO/SLA compliance

---

## Incident Response

### Severity Levels

| Level | Definition | Response Time | Example |
|-------|-----------|----------------|---------|
| **P1** | Critical production issue | Immediate | Critical security vulnerability |
| **P2** | Major feature broken | 1 hour | Build pipeline completely down |
| **P3** | Degraded functionality | 4 hours | Test failures, coverage drops |
| **P4** | Minor issue | Next business day | Documentation typo |

### Incident Response Process

```
Detection → Assessment → Response → Mitigation → Resolution → Post-Mortem
```

#### 1. Detection

- GitHub Actions workflow fails ⚠️
- SonarCloud quality gate fails ❌
- Security scan finds vulnerability 🔒
- Consumer reports issue
- Manual alert triggered

#### 2. Assessment

```bash
# 1. Classify severity level (P1-P4)
# 2. Determine impact scope:
#    - Does it block builds? P2
#    - Does it block releases? P2
#    - Does it affect security? P1
#    - Does it affect quality gates? P3

# 3. Identify root cause
# 4. Notify stakeholders if P1/P2
```

#### 3. Response

**P1/P2**: Immediate action
- Page on-call engineer
- Start incident channel (Slack/Teams)
- Begin investigation
- Update status regularly

**P3/P4**: Next working hours
- Log issue in ticket system
- Assign to engineer
- Plan fix in next sprint

#### 4. Mitigation

Apply temporary fix to restore service:

```bash
# For build failures:
git log --oneline | head -5  # Check recent changes
git diff HEAD~1              # What changed?

# For test failures:
dotnet test -v normal        # Run with detailed output
# Find which test failed, investigate cause

# For security issues:
# Check GitHub security tab
# Review SonarCloud SAST findings
# Consult DEVOPS_RUNBOOK.md emergency procedures
```

#### 5. Resolution

Apply permanent fix:

```bash
# 1. Create fix on feature branch
git checkout -b fix/incident-description

# 2. Make changes and test
dotnet build
dotnet test

# 3. Commit with clear message
git commit -m "Fix: [description]"

# 4. Push and merge
git push origin fix/incident-description
# Create PR, wait for checks, merge

# 5. Tag release if needed
git tag -a "v$(cat version-library.txt)" -m "Hotfix"
```

#### 6. Post-Mortem

After resolution:

```markdown
## Incident Post-Mortem

**Date**: YYYY-MM-DD
**Duration**: HH minutes
**Severity**: P1/P2/P3/P4

### What Happened?
[Description of incident]

### Root Cause
[Why did it happen?]

### Detection Time
[When was it detected?]

### Resolution Time
[When was it fixed?]

### Impact
[What was affected?]
[How many users/systems affected?]

### Action Items
- [ ] Task 1: Owner, Target date
- [ ] Task 2: Owner, Target date

### Lessons Learned
- What went well?
- What could improve?
- How can we prevent this?
```

---

## Emergency Procedures

### Build Pipeline Failure

```bash
# 1. Check GitHub Actions logs
# URL: https://github.com/nsschultz/fantasy-baseball-common-backend/actions

# 2. Identify which workflow failed
# - pr-validation.yml (build, test, formatting, security, and quality gate)
# - nuget-package.yml (package publish)
# - docker-image.yml (image build)

# 3. View failure logs
# - Click workflow run
# - Click failed job
# - Read error output

# 4. Common failures and fixes:

# A. "dotnet build" fails
#    → Check for syntax errors
#    → Verify .NET version
#    → Check dependencies: dotnet restore

# B. "dotnet test" fails
#    → Run locally: dotnet test
#    → Check test output
#    → Revert recent changes if new failure

# C. "SonarScanner" fails
#    → Check SONAR_TOKEN secret
#    → Verify project key
#    → Check Java installation

# D. "Docker build" fails
#    → Check Dockerfile syntax
#    → Verify base image availability
#    → Check build context

# 5. Fix and commit
git add .
git commit -m "Fix: [description of fix]"
git push origin main

# 6. Verify workflow passes
# Monitor GitHub Actions until ✅ green
```

### Critical Security Vulnerability Found

```bash
# 1. Assess severity
# - Critical = Immediate action
# - High = Today
# - Medium = This week

# 2. Check SonarCloud
# URL: https://sonarcloud.io/project/security?id=nsschultz_fantasy-baseball-common-backend

# 3. For dependency vulnerabilities
# - Check GitHub security tab
# - Review Dependabot alerts
# - Update affected dependency

# 4. For code vulnerabilities (SAST findings)
# - Review SonarCloud hotspot
# - Understand risk
# - Apply security fix

# 5. For container vulnerabilities (Trivy)
# - Check Dockerfile
# - Update base image if needed
# - Add vulnerability exclusion if false positive

# 6. If blocking release
# Must be fixed before release:
git checkout -b security/fix-description

# Make security fix
dotnet test  # Verify all tests pass

git add .
git commit -m "Security: Fix [CVE-XXXX or description]"
git push origin main

# 7. Coordinate release
# If critical, may need emergency hotfix release
# Follow DEPLOYMENT.md hotfix procedures
```

### Test Coverage Drops Below 80%

```bash
# 1. Identify which tests/code is missing coverage
# Check SonarCloud:
# https://sonarcloud.io/project/coverage?id=nsschultz_fantasy-baseball-common-backend

# 2. Determine if intentional
# - New code added without tests? → Add tests
# - Exclusion removed? → Re-add exclusion
# - Test failure? → Fix test

# 3. Add missing test coverage
git checkout -b test/improve-coverage

# Write tests for uncovered code
dotnet test

# Verify coverage improves
# Run with coverage report:
dotnet test /p:CollectCoverage=true

# 4. Commit tests
git add .
git commit -m "Test: Improve coverage for [feature]"
git push origin main

# 5. Monitor next SonarCloud scan
# Verify coverage back above 80%
```

### Package/Image Distribution Failure

```bash
# 1. Check distribution status

# NuGet Package:
# URL: https://github.com/nsschultz/fantasy-baseball-common-backend/packages

# Docker Image:
# URL: https://hub.docker.com/r/nschultz/fantasy-baseball-common-backend

# 2. If package not appearing

# A. Check GitHub Actions workflow
# - nuget-package.yml publishes on push to main
# - Check for publish step errors

# B. Verify GitHub Packages is enabled
# - Repo Settings → Packages → Enable

# C. Check credentials
# - GITHUB_TOKEN must have write:packages scope

# D. Verify package name/version format
VERSION=$(cat version-library.txt)
echo "Expected version: $VERSION"
# Should match pattern: X.Y.Z

# 3. If image not appearing

# A. Check GitHub Actions workflow
# - docker-image.yml status

# B. Verify Docker Hub credentials
# - DOCKERHUB_USERNAME set
# - DOCKERHUB_TOKEN valid (not password)

# C. Check image push step
# - Image name: nschultz/fantasy-baseball-common-backend
# - Tags applied correctly

# 4. Manual intervention (if needed)
# For NuGet:
dotnet nuget push "nupkgs/*.nupkg" \
  --source "https://nuget.pkg.github.com/nsschultz/index.json" \
  --api-key "$GITHUB_TOKEN"

# For Docker:
docker push nschultz/fantasy-baseball-common-backend:VERSION
docker push nschultz/fantasy-baseball-common-backend:latest
```

---

## SLO/SLA Compliance

### Current Targets

| SLO | Target | Monitored | Response |
|-----|--------|-----------|----------|
| **Build Success Rate** | 99.9% | GitHub Actions | P1 if < 99.9% |
| **Quality Gate Pass Rate** | 100% | SonarCloud | P2 if fails |
| **Test Coverage** | ≥80% | SonarCloud | P3 if drops |
| **Security Issues** | 0 Critical | SonarCloud/Trivy | P1 if Critical |
| **Package Availability** | 99.5% | GitHub Packages | P2 if unavailable |
| **Image Availability** | 99.5% | Docker Hub | P2 if unavailable |

### Monitoring Dashboards

Check these dashboards daily:

1. **GitHub Actions**
   - URL: https://github.com/nsschultz/fantasy-baseball-common-backend/actions
   - Check: All workflows should be ✅
   - Frequency: Real-time monitoring

2. **SonarCloud**
   - URL: https://sonarcloud.io/project/overview?id=nsschultz_fantasy-baseball-common-backend
   - Check: Quality gate passing, coverage ≥80%
   - Frequency: Per commit (real-time)

3. **GitHub Packages**
   - URL: https://github.com/nsschultz/fantasy-baseball-common-backend/packages
   - Check: Latest version available
   - Frequency: After each release

4. **Docker Hub**
   - URL: https://hub.docker.com/r/nschultz/fantasy-baseball-common-backend
   - Check: Latest version tagged, both platforms present
   - Frequency: After each release

### Weekly SLO Review

Every Monday morning:

```bash
# 1. Check GitHub Actions success rate
# - Should be 99.9% or higher
# - Note any failures and root causes

# 2. Review SonarCloud metrics
# - Quality gate: Passing ✅
# - Coverage: ≥80% ✅
# - No new vulnerabilities ✅

# 3. Verify distribution channels
# - NuGet: Latest version available
# - Docker: Latest version, both platforms

# 4. Document in SLO log
# Date: YYYY-MM-DD
# Build Success: 99.9% ✅
# Quality Gate: Passing ✅
# Coverage: 85% ✅
# Security: 0 Critical ✅
# Availability: 100% (no downtime) ✅

# 5. Identify trends
# - Is coverage trending up or down?
# - Are build failures increasing?
# - New security concerns?

# 6. Plan improvements if needed
```

---

## Common Issues & Solutions

### Issue: "dotnet build" fails with "CS0246: The type or namespace name could not be found"

**Cause**: Missing NuGet package or namespace

**Solution**:
```bash
# 1. Restore packages
dotnet restore

# 2. Check project file
cat FantasyBaseball.Common/FantasyBaseball.Common.csproj

# 3. Verify reference
# Look for: <ItemGroup> <PackageReference Include="..." />

# 4. Rebuild
dotnet build
```

### Issue: "dotnet test" fails with "no tests found"

**Cause**: Test project not discovered or naming issue

**Solution**:
```bash
# 1. Verify test project exists
ls FantasyBaseball.Common.UnitTests/

# 2. Check test file naming
# Should be: *Tests.cs or *Test.cs

# 3. Check test class attributes
# Should have: [Fact] or [Theory] on test methods

# 4. Run tests explicitly
dotnet test FantasyBaseball.Common.UnitTests/FantasyBaseball.Common.UnitTests.csproj

# 5. Check for compilation errors first
dotnet build FantasyBaseball.Common.UnitTests/
```

### Issue: SonarCloud reports "Coverage is below 80%"

**Cause**: New code without tests or excluded code

**Solution**:
```bash
# 1. Identify uncovered code
# Check SonarCloud Coverage tab

# 2. Add tests for new code
git checkout -b test/coverage-improvement

# Write tests
dotnet test /p:CollectCoverage=true

# 3. Or exclude if intentional
# In scan.sh:
# /d:sonar.coverage.exclusions="**/SomeClass.cs"

# 4. Re-run analysis
sh .docker-compose/ci.sh
# Test results are extracted to .docker-compose/test-results/
```

### Issue: Docker build fails with "FROM image not found"

**Cause**: Base image unavailable or registry issue

**Solution**:
```bash
# 1. Check Dockerfile
cat Dockerfile
# Check: FROM line for correct image

# 2. Pull base image
docker pull mcr.microsoft.com/dotnet/sdk:10.0

# 3. Check Docker login
docker login

# 4. Retry build
docker build -t fantasy-baseball-common-backend .
```

### Issue: GitHub Actions workflow stuck in "queued" status

**Cause**: Runner unavailable or configuration issue

**Solution**:
```bash
# 1. Check Actions settings
# Repo Settings → Actions → Runners

# 2. Verify runner is online
# Green status = online

# 3. Cancel and re-run
# On workflow run: "Cancel workflow run"
# Then: "Re-run failed jobs" or "Re-run all jobs"

# 4. Check for runner limits
# GitHub free tier: Limited concurrent jobs
```

---

## Daily Operational Checklist

### Start of Day (Morning)

- [ ] Check GitHub Actions dashboard
  - Are all recent workflows ✅ or ❌?
  - Any failed builds to investigate?
- [ ] Review SonarCloud metrics
  - Quality gate status?
  - Coverage trend?
  - New issues?
- [ ] Check security alerts
  - GitHub security tab
  - Any vulnerabilities?

### During Day

- [ ] Monitor incoming alerts
  - Build failures
  - Security issues
  - Coverage drops
- [ ] Respond to issues promptly
  - Investigate root cause
  - Communicate status
  - Apply fix

### End of Day (Evening)

- [ ] Verify all builds are green
- [ ] Document any incidents
- [ ] Note trends or concerns
- [ ] Prepare for next on-call

---

## Escalation Path

### Who to Contact

| Issue | Contact | Channel | Response Time |
|-------|---------|---------|----------------|
| Build Failure | Dev Lead | Slack | 30 min |
| Security Vulnerability | Security Team | Slack #security | 1 hour |
| Release Approval | Tech Lead | Email | 2 hours |
| Infrastructure Issue | DevOps Lead | Slack #devops | 1 hour |
| Consumer Issue | Tech Lead | Email | 4 hours |

### On-Call Schedule

To be determined by team. Typically:
- 1-week rotation
- Monday → Sunday
- Handoff: Weekly sync meeting

---

## Runbook Quick Links

**Key URLs**:
- GitHub Repo: https://github.com/nsschultz/fantasy-baseball-common-backend
- GitHub Actions: https://github.com/nsschultz/fantasy-baseball-common-backend/actions
- SonarCloud: https://sonarcloud.io/project/overview?id=nsschultz_fantasy-baseball-common-backend
- GitHub Packages: https://github.com/nsschultz/fantasy-baseball-common-backend/packages
- Docker Hub: https://hub.docker.com/r/nschultz/fantasy-baseball-common-backend

**Key Files**:
- DEPLOYMENT.md - Release procedures
- SLO_SLA.md - Service level definitions
- CHANGELOG.md - Release history
- Dockerfile - Build configuration
- scan.sh - Quality scan script

**Key Commands**:
```bash
# Build
dotnet build

# Test
dotnet test

# Test with coverage
dotnet test /p:CollectCoverage=true

# Package
dotnet pack FantasyBaseball.Common/FantasyBaseball.Common.csproj --configuration Release

# Docker build
docker build -t nschultz/fantasy-baseball-common-backend:VERSION .

# Docker run with CI environment
sh .docker-compose/ci.sh
```

---

**Last Updated**: 2026-11-18  
**Maintained By**: DevOps Team  
**Next Review**: 2026-12-15
