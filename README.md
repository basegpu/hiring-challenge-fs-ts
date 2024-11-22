# Fullstack Time Series Challenge

The original challenge is described [here](./challenge/README.md).

## Approach to solution
The idea is to have the most simple solution that "just works".
From there multiple improvements will/can be made.

### Streamlit app
The frontend is implemented in Python with Streamlit. See [the Streamlit project](./src/streamlit).

1. Single page application with streamlit
2. data is loaded into memory
3. dropdowns with checkboxes for assets and signals
4. map of assets (and colored by selected signals)
5. plotly chart with time series
6. export button

### Containerization
Using docker and docker compose. See all Dockerfiles and the [compose file](./src/compose.yaml).

- run app with `docker compose up --build app-local`
- run app with debugger `docker compose up --build app-local-debug`

### REST API for adding and retrieving data
The backend API is implemented in .NET Core. See [the API project](./src/api).

- run app pointing to API with in-memory data with `docker compose up --build app-api-local`
- run app pointing to API with in-memory data and debugger with `docker compose up --build app-api-local-debug`

The endpoints are:
- `GET /api/assets` (all assets)
- `GET /api/assets/{assetId}` (single asset)
- `GET /api/signals` (all signals)
- `GET /api/signals?assetId={assetId}` (all signals for asset)
- `GET /api/signals/{signalId}` (single signal)
- `GET /api/data?signalId={signalId}&from={from}&to={to}` (data for signal)

potentially for adding data:
- `POST /api/assets`
- `POST /api/signals`

### DB for storing assets and signals
The DB schema is implemented in Postgres. See [the DB project](./src/db).

- run app pointing to api and DB with `docker compose up --build app-api-db`
- run app pointing to api and DB with debugger `docker compose up --build app-api-db-debug`

The data is read from the same files as in the original challenge, inserted into the DB upon initialization.

### Not covered yet
- Add frontend framework (typescript/react)
- Add message queue for realtime data updates (rabbitmq) - published by write endpoints in API

## Some thoughts on testing
Testing in its various forms is important and should be carefully laid out. Having tests for the sake of having tests is not the solution.

Different test setups needed for this challenge:
- Unit tests for API (nunit)
- Integration tests for API (dotnet testclient, mocking db with in-memory repository)
- Load testing for API (postman collection, docker compose)
- Unit tests for Streamlit app (pytest)
- Integration tests for Streamlit app (cypress, docker compose)

Because having test setups for the different testing levels takes time, I focused on the most important one - api testing.


## Time spent

| Date       | Hours | What |
|------------|-------|------|
| 2024-11-14 | 0.5 | familiarize with challenge, planning |
| 2024-11-14 | 1.0 | Streamlit app, vscode debugging |
| 2024-11-14 | 0.5 | Containerization, Dockerfile, compose file |
| 2024-11-15 | 2.0 | REST API, API project |
| 2024-11-15 | 1.0 | hooking-up app to API |
| 2024-11-17 | 1.0 | DB project |
| 2024-11-17 | 0.5 | hooking up DB to API |

total: 6.5 hours, plus some offline thinking (not tracked, maybe 1 hour)