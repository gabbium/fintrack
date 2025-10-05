FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

RUN apt-get update && apt-get install -y --no-install-recommends curl ca-certificates && rm -rf /var/lib/apt/lists/*

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]

COPY ["services/ServiceName/src/Fintrack.ServiceName.MigrationService/Fintrack.ServiceName.MigrationService.csproj", "services/ServiceName/src/Fintrack.ServiceName.MigrationService/"]
COPY ["services/ServiceName/src/Fintrack.ServiceName.Infrastructure/Fintrack.ServiceName.Infrastructure.csproj", "services/ServiceName/src/Fintrack.ServiceName.Infrastructure/"]
COPY ["services/ServiceName/src/Fintrack.ServiceName.Application/Fintrack.ServiceName.Application.csproj", "services/ServiceName/src/Fintrack.ServiceName.Application/"]
COPY ["services/ServiceName/src/Fintrack.ServiceName.Domain/Fintrack.ServiceName.Domain.csproj", "services/ServiceName/src/Fintrack.ServiceName.Domain/"]
COPY ["ops/Fintrack.ServiceDefaults/Fintrack.ServiceDefaults.csproj", "ops/Fintrack.ServiceDefaults/"]

RUN dotnet restore "services/ServiceName/src/Fintrack.ServiceName.MigrationService/Fintrack.ServiceName.MigrationService.csproj"

COPY . .

WORKDIR "/src/services/ServiceName/src/Fintrack.ServiceName.MigrationService"
RUN dotnet build "Fintrack.ServiceName.MigrationService.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Fintrack.ServiceName.MigrationService.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fintrack.ServiceName.MigrationService.dll"]
