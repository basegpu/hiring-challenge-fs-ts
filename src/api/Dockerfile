FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY TsApi/*.csproj .
RUN dotnet restore

COPY TsApi/. .
RUN dotnet build -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "TsApi.dll"]
