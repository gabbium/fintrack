FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

RUN apt-get update && apt-get install -y --no-install-recommends curl ca-certificates && rm -rf /var/lib/apt/lists/*

WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]

COPY ["workloads/Ledger/src/Fintrack.Ledger.API/Fintrack.Ledger.API.csproj", "workloads/Ledger/src/Fintrack.Ledger.API/"]
COPY ["workloads/Ledger/src/Fintrack.Ledger.Infrastructure/Fintrack.Ledger.Infrastructure.csproj", "workloads/Ledger/src/Fintrack.Ledger.Infrastructure/"]
COPY ["workloads/Ledger/src/Fintrack.Ledger.Application/Fintrack.Ledger.Application.csproj", "workloads/Ledger/src/Fintrack.Ledger.Application/"]
COPY ["workloads/Ledger/src/Fintrack.Ledger.Domain/Fintrack.Ledger.Domain.csproj", "workloads/Ledger/src/Fintrack.Ledger.Domain/"]
COPY ["aspire/src/Fintrack.ServiceDefaults/Fintrack.ServiceDefaults.csproj", "aspire/src/Fintrack.ServiceDefaults/"]

RUN dotnet restore "workloads/Ledger/src/Fintrack.Ledger.API/Fintrack.Ledger.API.csproj"

COPY . .

WORKDIR "/src/workloads/Ledger/src/Fintrack.Ledger.API"
RUN dotnet build "Fintrack.Ledger.API.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Fintrack.Ledger.API.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fintrack.Ledger.API.dll"]
