#!/bin/bash
set -e
EXIT_CODE=0
docker compose -f docker-compose.yaml -p fantasy-baseball-common-backend up --build --exit-code-from api || EXIT_CODE=$?
docker compose -f docker-compose.yaml -p fantasy-baseball-common-backend down
exit $EXIT_CODE