FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

RUN apt-get update && apt-get install -y --no-install-recommends curl ca-certificates && rm -rf /var/lib/apt/lists/*

WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]

COPY ["services/Planning/src/Fintrack.Planning.Api/Fintrack.Planning.Api.csproj", "services/Planning/src/Fintrack.Planning.Api/"]
COPY ["services/Planning/src/Fintrack.Planning.Infrastructure/Fintrack.Planning.Infrastructure.csproj", "services/Planning/src/Fintrack.Planning.Infrastructure/"]
COPY ["services/Planning/src/Fintrack.Planning.Application/Fintrack.Planning.Application.csproj", "services/Planning/src/Fintrack.Planning.Application/"]
COPY ["services/Planning/src/Fintrack.Planning.Domain/Fintrack.Planning.Domain.csproj", "services/Planning/src/Fintrack.Planning.Domain/"]
COPY ["aspire/Fintrack.ServiceDefaults/Fintrack.ServiceDefaults.csproj", "aspire/Fintrack.ServiceDefaults/"]

RUN dotnet restore "services/Planning/src/Fintrack.Planning.Api/Fintrack.Planning.Api.csproj"

COPY . .

WORKDIR "/src/services/Planning/src/Fintrack.Planning.Api"
RUN dotnet build "Fintrack.Planning.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Fintrack.Planning.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fintrack.Planning.Api.dll"]
