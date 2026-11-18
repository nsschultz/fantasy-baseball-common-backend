#!/bin/bash
# scan.sh - Comprehensive test and coverage scanning for multiple test projects
# 
# This script:
# 1. Discovers all test projects matching *Tests.csproj or *Test.csproj patterns
# 2. Runs tests for each project with individual coverage collection
# 3. Merges coverage reports from all projects into a unified report
# 4. Publishes release artifacts
# 5. Sends aggregated coverage and quality metrics to SonarCloud
#
# Environment variables required:
#   SERVICE - Service name for SonarCloud (e.g., fantasy-baseball-common-backend)
#   SONAR_TOKEN - Authentication token for SonarCloud
#   VERSION - Version string for tagging releases
#
# Output directories:
#   coverage-results/ - Merged coverage reports in OpenCover format
#   TestResults/ - Individual test result files (.trx) for all test projects

set -e

rm -rf coverage-results/
rm -rf TestResults/
mkdir -p coverage-results/
mkdir -p TestResults/

dotnet sonarscanner begin \
  /o:"nsschultz" \
  /k:nsschultz_$SERVICE \
  /n:$SERVICE \
  /d:sonar.token="$SONAR_TOKEN" \
  /v:$VERSION \
  /d:sonar.cs.opencover.reportsPaths=coverage-results/coverage.opencover.xml \
  /d:sonar.dotnet.excludeTestProjects=true \
  /d:sonar.coverage.exclusions="**/Database/Migrations/*.cs" \
  /d:sonar.exclusions="**/Database/Migrations/*.cs" \
  /d:sonar.host.url="https://sonarcloud.io"

# Find and process all test projects
find . -type f \( -name "*Tests.csproj" -o -name "*Test.csproj" \) | while read test_project; do
  test_dir=$(dirname "$test_project")
  project_name=$(basename "$test_dir")
  
  echo "Running tests for: $project_name"
  
  dotnet test "$test_project" \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutput=../../coverage-results/$project_name/" \
    "/p:CoverletOutputFormat=\"json,opencover\"" \
    "/p:MergeWith=../../coverage-results/$project_name/coverage.json" \
    --logger "trx;LogFileName=$project_name.trx" \
    --results-directory TestResults \
    --no-build
done

# Merge coverage reports from all test projects into a unified OpenCover report
if [ -d "coverage-results" ] && [ "$(find coverage-results -name 'coverage.opencover.xml' | wc -l)" -gt 0 ]; then
  echo "Merging coverage reports..."
  dotnet tool install -g dotnet-reportgenerator-globaltool --version 5.3.14 || true
  
  reportgenerator \
    -reports:coverage-results/*/coverage.opencover.xml \
    -targetdir:coverage-results \
    -reporttypes:OpenCoverSummary \
    -assemblyfilters:"-xunit*"
fi

dotnet publish -c Release -o /app/out -v minimal
dotnet sonarscanner end /d:sonar.token="$SONAR_TOKEN"
