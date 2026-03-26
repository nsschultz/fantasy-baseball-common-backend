[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nsschultz_fantasy-baseball-common-backend&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=nsschultz_fantasy-baseball-common-backend)

## Common Backend

- This is commond code shared between the backend services.

---

### Build Image

```
version=$(cat version-image.txt) && docker build -t nschultz/fantasy-baseball-common-backend:$version .
```

---

### Dev Container

- In VS Code, use the "Clone Repository into Container Volume..." option to open the workspace.
- Tasks are setup in tasks.json.
