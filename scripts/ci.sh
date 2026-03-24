#!/bin/bash
set -e
EXIT_CODE=0
docker compose -f .docker-compose/docker-compose-ci.yaml -p ${SERVICE} up --build --exit-code-from api || EXIT_CODE=$?
docker compose -f .docker-compose/docker-compose-ci.yaml -p ${SERVICE} down
docker volume rm ${SERVICE}_data_volume
exit $EXIT_CODE