[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nsschultz_fantasy-baseball-common-backend&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=nsschultz_fantasy-baseball-common-backend)

## Common Backend

- This is commond code shared between the backend services.

---

### Build Images

```
version=$(cat version-image.txt) && docker build -t nschultz/fantasy-baseball-common-backend-build:$version .
version=$(cat version-image.txt) && docker build -t nschultz/fantasy-baseball-common-backend-ci:$version .
```

---

### Dev Container

- VS Code should auto-prompt to reopen the workspace in a contaienr, which will start the rest of the containers as well.
- Tasks are setup in tasks.json.
- Command for manually starting/stopping dev containers:

```
docker compose -f docker-compose.yaml -p fantasy-baseball-common-backend up --build -d
docker compose -f docker-compose.yaml -p fantasy-baseball-common-backend down
```
