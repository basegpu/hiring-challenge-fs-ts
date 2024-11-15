# Fullstack Time Series Challenge

The original challenge is described [here](./challenge/README.md).

## Most simple solution

The idea is to have the most simple solution that "just works". From there multiple improvements will/can be made.

1. Single page application with streamlit
2. data is loaded into memory
3. dropdowns with checkboxes for assets and signals
4. plotly chart with time series
5. export button

## Improvements

### Containerization
Using docker and docker compose. See [the Dockerfile](./Dockerfile) and [the compose file](./compose.yaml).

- run app with `docker compose up --build ts-viewer`
- run app with debugger `docker compose up --build ts-viewer-debug`

### REST API for adding and retrieving data
The backend API is implemented in .NET Core. See [the API project](./api).

The endpoints are:
- `GET /api/assets` (all assets)
- `GET /api/assets/{assetId}` (single asset)
- `GET /api/signals` (all signals)
- `GET /api/signals?assetId={assetId}` (all signals for asset)
- `GET /api/signals/{signalId}` (single signal)

potentially for adding data:
- `POST /api/assets`
- `POST /api/signals`

### Not covered yet
- Add DB for storing assets and signals (postgres)
- Add frontend framework (typescript/react)
- Add message queue for realtime data updates (rabbitmq)

## Time spent

| Date       | Hours | What |
|------------|-------|------|
| 2024-11-14 | 0.5 | familiarize with challenge, planning |
| 2024-11-14 | 1.0 | Streamlit app, vscode debugging |
| 2024-11-14 | 0.5 | Containerization, Dockerfile, compose file |

