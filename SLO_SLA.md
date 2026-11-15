# Service Level Objectives (SLO) and Service Level Agreements (SLA)

## Overview

This document defines the service level objectives (SLOs) for the fantasy-baseball-common-backend library and service level agreements (SLAs) with consumers.

---

## Service Level Objectives (SLOs)

SLOs are internal targets that guide our operational decisions and resource allocation.

### 1. Availability SLO

**Target**: 99.5% uptime for package distribution  
**Error Budget**: ~3.6 hours per month of acceptable downtime

**Definition**:
- GitHub Packages availability for NuGet package downloads
- Docker Hub availability for image pulls
- Measured: Monthly uptime percentage

**Measurement**:
```
Availability % = (Total Minutes - Downtime Minutes) / Total Minutes × 100
```

**Scope**:
- Includes: GitHub Packages, Docker Hub infrastructure
- Excludes: Consumer network/firewall issues
- Excludes: Authentication failures

**Monitoring**:
- GitHub status dashboard
- Docker Hub status page
- Automated alerting if availability drops below 99%

---

### 2. Performance SLO

**Target**: P95 download latency < 500ms  
**Measurement Period**: Per-request, aggregated daily

**Definition**:
- Time from download initiation to completion for NuGet packages
- Time from image pull initiation to completion for Docker images
- Percentile: 95th percentile (95% of requests faster than target)

**Measurement**:
```
P95 Latency = 95th percentile of all request latencies
```

**Targets by Package Type**:
| Package Type | Size | Target P95 |
|--------------|------|-----------|
| NuGet Package | ~500KB | 200ms |
| Docker Image (amd64) | ~1.5GB | 500ms |
| Docker Image (arm64) | ~1.5GB | 500ms |

**Monitoring**:
- GitHub Packages download logs (if available)
- Docker Hub analytics
- Consumer feedback on download speed

---

### 3. Reliability SLO

**Target**: 99.9% CI/CD pipeline success rate  
**Error Budget**: ~43 minutes per month of acceptable failures

**Definition**:
- Success rate of automated build, test, and deployment workflows
- Excludes: External infrastructure failures (GitHub outage)
- Excludes: Intentional workflow skips

**Measurement**:
```
Success Rate = (Total Runs - Failed Runs) / Total Runs × 100
```

**What's Tracked**:
- GitHub Actions workflow execution success
- Build step completion
- Test execution completion
- Package/image publishing success

**Quality Gates**:
- Build must pass
- All tests must pass (0 failures)
- Code quality gates must pass
- Security checks must pass

**Targets**:
- Overall success rate: 99.9%
- Build step: 99.95%
- Test step: 99.95%
- Publish step: 99.8%

**Monitoring**:
- GitHub Actions dashboard
- Workflow run history
- Automated alerts on failure spike

---

### 4. Quality SLO

**Target**: Code coverage ≥80%  
**Measurement Period**: Per-commit, aggregated per release

**Definition**:
- Lines of code executed by unit tests
- Measured by SonarCloud
- Excludes: Database migrations
- Excludes: Auto-generated code

**Measurement**:
```
Coverage % = (Lines Covered by Tests / Total Lines) × 100
```

**Targets**:
| Component | Minimum Coverage |
|-----------|-----------------|
| Overall Project | ≥80% |
| Public APIs | ≥90% |
| Business Logic | ≥85% |
| Utilities | ≥80% |
| Exceptions | ≥75% |

**Quality Gate Requirements**:
- Coverage must not decrease from previous version
- New code must be covered
- Critical paths must have high coverage

**Monitoring**:
- SonarCloud coverage report
- Per-PR coverage check
- Weekly trend analysis

---

### 5. Security SLO

**Target**: Zero critical vulnerabilities  
**Measurement Period**: Per-commit

**Definition**:
- Static Application Security Testing (SAST) findings
- Container image vulnerabilities (Trivy)
- Dependency vulnerabilities
- Severity levels: CRITICAL, HIGH, MEDIUM, LOW, INFO

**Vulnerabilities Tracked**:
| Tool | Type | Target |
|------|------|--------|
| SonarQube SAST | Code vulnerabilities | 0 CRITICAL |
| Trivy | Container issues | 0 CRITICAL |
| Dependabot | Dependencies | 0 CRITICAL |
| GitHub Security | Advisories | 0 CRITICAL |

**Acceptable Thresholds**:
| Severity | Target | Action |
|----------|--------|--------|
| CRITICAL | 0 | Block release, immediate fix |
| HIGH | 0 | Fix before release |
| MEDIUM | TBD by team | Plan fix in next sprint |
| LOW | TBD by team | Plan fix later |

**Monitoring**:
- SonarCloud security hotspots
- GitHub security alerts
- Trivy scan results
- Dependabot reports

---

### 6. Maintainability SLO

**Target**: SonarCloud quality gate passing  
**Measurement Period**: Per-commit

**Definition**:
- Code maintainability metrics tracked by SonarCloud
- Includes: Code smells, debt, complexity
- Quality gate: PASSING ✅

**Quality Gate Criteria**:
- Maintainability rating: A or better
- No new issues introduced
- Technical debt ratio acceptable
- Code coverage maintained

**Targets**:
- Maintainability Rating: A
- Code Smell Density: < 1 per 100 LOC
- Complexity: Reasonable (no cyclomatic complexity > 10)

**Monitoring**:
- SonarCloud quality gate status
- Per-PR quality gate check
- Technical debt trending

---

## Service Level Agreements (SLAs)

SLAs are commitments to consumers (internal teams, partner organizations).

### 1. NuGet Package SLA

**Commitment**:
- Available in GitHub Packages within 10 minutes of release
- 99.5% monthly availability
- P95 download latency < 200ms

**Guarantee Period**: 
- Starts: Version tag published
- Ends: Package deprecated or removed

**Exclusions**:
- GitHub Packages infrastructure maintenance (planned)
- Network issues outside our control
- Consumer authentication/permission issues

**Remedy if SLA Breached**:
- Hotfix release on next business day
- Post-incident review
- Communication to affected consumers

---

### 2. Docker Image SLA

**Commitment**:
- Available on Docker Hub within 10 minutes of release
- Images available for both linux/amd64 and linux/arm64 platforms
- 99.5% monthly availability
- P95 pull latency < 500ms

**Guarantee Period**:
- Starts: Version tag published
- Ends: Image deprecated or removed

**Exclusions**:
- Docker Hub infrastructure maintenance (planned)
- Network issues outside our control
- Consumer authentication/permission issues

**Remedy if SLA Breached**:
- Hotfix release on next business day
- Post-incident review
- Communication to affected consumers

---

### 3. Release SLA

**Commitment**:
- Releases occur on a regular schedule (1-2 per month)
- All released versions tested (145+ test cases)
- Code coverage ≥80%
- Quality gates passing
- No CRITICAL security vulnerabilities

**Guarantee Period**:
- Applies to all releases on main branch
- Starts: Release tag created
- Ends: Next release supersedes

**Exclusions**:
- Emergency/security releases (may be ad-hoc)
- Planned maintenance windows
- Force majeure events

**Remedy if SLA Breached**:
- Hotfix release on next business day
- Root cause analysis
- Process improvement to prevent recurrence

---

### 4. Quality SLA

**Commitment**:
- Code coverage maintained at ≥80%
- Quality gate always passing
- Zero CRITICAL security vulnerabilities in released versions
- All tests passing (0 failures)

**Guarantee Period**:
- Applies to all code on main branch
- Continuous measurement

**Exclusions**:
- External dependencies with critical issues
- GitHub/SonarCloud outages

**Remedy if SLA Breached**:
- Hotfix within 24 hours
- Coverage gap analysis
- Test coverage improvements planned

---

## Error Budgets

Error budget defines acceptable downtime/failures before SLO breach.

### Availability Error Budget (99.5% SLO)

```
Total Minutes per Month: 43,200 minutes
Target Uptime: 99.5%
Acceptable Downtime: 216 minutes (~3.6 hours)

Daily Budget: ~7.2 minutes
Weekly Budget: ~51.4 minutes
```

**Usage**:
- GitHub Packages maintenance: 30 min allowed
- Docker Hub maintenance: 30 min allowed
- Emergency fixes: 60 min allowed
- Unplanned outages: 60 min allowed

**Tracking**:
```
Downtime Used = Incident Duration + Related Impact
Remaining Budget = 216 min - Downtime Used
```

### Reliability Error Budget (99.9% CI/CD SLO)

```
Total Workflows per Month: ~400 (assuming daily pushes)
Target Success Rate: 99.9%
Acceptable Failures: 0.4 failures (~1 failure per month)

Weekly Budget: ~0.1 failures (very strict)
```

**Usage**:
- Expected occasional failures due to:
  - External service issues (GitHub, Docker Hub)
  - Dependency problems
  - Transient network issues

**Tracking**:
- Monitor failure rate trend
- Alert if exceeding 1 per month

---

## SLO Monitoring & Reporting

### Daily Monitoring

**Morning Check** (9 AM):
```bash
1. GitHub Actions status
   - Any failed workflows overnight?
   - All recent builds green?

2. SonarCloud status
   - Quality gate passing?
   - Coverage maintained?
   - New security issues?

3. Distribution channels
   - NuGet package accessible?
   - Docker images pullable?
```

**Ongoing**:
- Automated alerts for SLO breaches
- Real-time dashboard monitoring
- Consumer issue tracking

### Weekly Review

**Every Monday, 10 AM**:

```bash
# Check metrics for previous week
Week: YYYY-MM-DD to YYYY-MM-DD

1. Availability Metric
   - Uptime: XX% (Target: 99.5%)
   - Incidents: N
   - Total downtime: X minutes
   - Budget remaining: X minutes

2. Performance Metric
   - P95 latency: XXms (Target: <500ms)
   - Trend: Improving/Stable/Degrading

3. Reliability Metric
   - Success rate: XX.X% (Target: 99.9%)
   - Failed runs: N
   - Budget remaining: XX failures

4. Quality Metric
   - Coverage: XX% (Target: ≥80%)
   - Quality gate: PASSING/FAILING
   - New issues: N
   - Trend: Improving/Stable/Degrading

5. Security Metric
   - CRITICAL vulnerabilities: N (Target: 0)
   - HIGH vulnerabilities: N
   - Trend: Improving/Stable/Degrading

6. Overall SLO Status
   - ON TRACK ✅ / AT RISK ⚠️ / BREACHED ❌
```

**Send Report To**:
- Tech Lead
- Development Team
- Any consumers with SLA agreements

### Monthly Review

**End of Month**:

```markdown
## Monthly SLO Report - [Month/Year]

### Key Metrics

| Metric | Target | Actual | Status |
|--------|--------|--------|--------|
| Availability | 99.5% | XX% | ✅/❌ |
| Performance | <500ms | XXms | ✅/❌ |
| Reliability | 99.9% | XX% | ✅/❌ |
| Quality | ≥80% | XX% | ✅/❌ |
| Security | 0 Critical | N | ✅/❌ |

### Incidents This Month

- Incident 1: [Date, Duration, Impact, Resolution]
- Incident 2: [Date, Duration, Impact, Resolution]

### Error Budget Usage

- Availability Budget Used: X/216 minutes
- Reliability Budget Used: X/1 failures
- Remaining Buffer: Sufficient/At Risk/Exhausted

### Trends

- Coverage: Trending Up ↑ / Stable → / Down ↓
- Build Success: Trending Up ↑ / Stable → / Down ↓
- Security: Trending Up (better) ↑ / Stable → / Down ↓

### Action Items

- [ ] Action 1: Owner, Target date
- [ ] Action 2: Owner, Target date

### Next Month Forecast

- Expected changes?
- Planned maintenance?
- Known risks?
```

### Quarterly Business Review

**Each Quarter**:
- Aggregate SLO performance over 3 months
- Identify trends and patterns
- Plan improvements
- Review commitments and adjust if needed
- Present to leadership/stakeholders

---

## SLO Breach Response

If any SLO is breached:

### Immediate Actions (< 1 hour)

1. Declare incident (P1 if critical)
2. Notify stakeholders
3. Begin investigation
4. Start incident log
5. Assess impact and severity

### Short-term Actions (< 24 hours)

1. Complete root cause analysis
2. Implement temporary fix if needed
3. Communicate resolution plan
4. Update consumers

### Resolution (< 7 days)

1. Apply permanent fix
2. Prevent recurrence
3. Close incident
4. Conduct post-mortem

### Learning (Ongoing)

1. Document what happened
2. Identify process improvements
3. Update procedures
4. Share learnings with team

---

## SLO Adjustment

SLOs may be adjusted if:

- Technology changes
- Consumer requirements evolve
- Infrastructure changes
- Market conditions shift

**Process**:
1. Propose change with business case
2. Review with team
3. Agree on new target
4. Update this document
5. Communicate to consumers
6. Update dashboards/alerts

---

## Dashboard URLs

For monitoring SLOs:

| Metric | Dashboard | Frequency |
|--------|-----------|-----------|
| Availability | GitHub Packages / Docker Hub status | Real-time |
| Reliability | [GitHub Actions](https://github.com/nsschultz/fantasy-baseball-common-backend/actions) | Real-time |
| Quality | [SonarCloud](https://sonarcloud.io/project/overview?id=nsschultz_fantasy-baseball-common-backend) | Per-commit |
| Security | [GitHub Security Tab](https://github.com/nsschultz/fantasy-baseball-common-backend/security) | Per-commit |

---

## Related Documentation

- **[DEPLOYMENT.md](./DEPLOYMENT.md)** - Release procedures
- **[DEVOPS_RUNBOOK.md](./DEVOPS_RUNBOOK.md)** - Operational procedures
- **[CHANGELOG.md](./CHANGELOG.md)** - Release history
- **[README.md](./README.md)** - Project overview

---

**Last Updated**: 2026-11-15  
**Next Review**: 2027-02-15 (Quarterly)  
**Maintained By**: DevOps Team

---

## Glossary

- **SLO**: Service Level Objective - internal target
- **SLA**: Service Level Agreement - external commitment
- **P95**: 95th percentile (95% of values are at or below this)
- **Error Budget**: Acceptable amount of downtime/failure
- **Quality Gate**: Set of checks that must pass
- **Coverage**: Percentage of code executed by tests
- **SAST**: Static Application Security Testing
