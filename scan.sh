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

# Build the code within the sonar context
dotnet build -c Release

# Find and process all test projects
find . -type f \( -name "*Tests.csproj" -o -name "*Test.csproj" \) | while read test_project; do
  test_dir=$(dirname "$test_project")
  project_name=$(basename "$test_dir")
  
  echo "Running tests for: $project_name"
  
  dotnet test "$test_project" \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutput=../../coverage-results/" \
    "/p:CoverletOutputFormat=\"json,opencover\"" \
    "/p:MergeWith=../../coverage-results/coverage.json" \
    --logger "trx;LogFileName=$project_name.trx" \
    --results-directory TestResults \
    --no-build
done

dotnet sonarscanner end /d:sonar.token="$SONAR_TOKEN"
