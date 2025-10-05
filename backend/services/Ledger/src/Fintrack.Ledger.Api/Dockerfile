FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

RUN apt-get update && apt-get install -y --no-install-recommends curl ca-certificates && rm -rf /var/lib/apt/lists/*

WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]

COPY ["services/Ledger/src/Fintrack.Ledger.Api/Fintrack.Ledger.Api.csproj", "services/Ledger/src/Fintrack.Ledger.Api/"]
COPY ["services/Ledger/src/Fintrack.Ledger.Infrastructure/Fintrack.Ledger.Infrastructure.csproj", "services/Ledger/src/Fintrack.Ledger.Infrastructure/"]
COPY ["services/Ledger/src/Fintrack.Ledger.Application/Fintrack.Ledger.Application.csproj", "services/Ledger/src/Fintrack.Ledger.Application/"]
COPY ["services/Ledger/src/Fintrack.Ledger.Domain/Fintrack.Ledger.Domain.csproj", "services/Ledger/src/Fintrack.Ledger.Domain/"]
COPY ["aspire/Fintrack.ServiceDefaults/Fintrack.ServiceDefaults.csproj", "aspire/Fintrack.ServiceDefaults/"]

RUN dotnet restore "services/Ledger/src/Fintrack.Ledger.Api/Fintrack.Ledger.Api.csproj"

COPY . .

WORKDIR "/src/services/Ledger/src/Fintrack.Ledger.Api"
RUN dotnet build "Fintrack.Ledger.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Fintrack.Ledger.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fintrack.Ledger.Api.dll"]
