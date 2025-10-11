FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

ARG APP_UID=64123
ARG APP_GID=64123
RUN groupadd --gid ${APP_GID} nonroot \
    && useradd  --uid ${APP_UID} --gid ${APP_GID} --create-home --shell /usr/sbin/nologin nonroot

WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Directory.Build.props ./
COPY Directory.Packages.props ./
COPY building-blocks/BuildingBlocks.Api/BuildingBlocks.Api.csproj building-blocks/BuildingBlocks.Api/
COPY building-blocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj building-blocks/BuildingBlocks.Infrastructure/
COPY building-blocks/BuildingBlocks.Application/BuildingBlocks.Application.csproj building-blocks/BuildingBlocks.Application/
COPY building-blocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj building-blocks/BuildingBlocks.Domain/
COPY services/Ledger/src/Fintrack.Ledger.Api/Fintrack.Ledger.Api.csproj services/Ledger/src/Fintrack.Ledger.Api/
COPY services/Ledger/src/Fintrack.Ledger.Infrastructure/Fintrack.Ledger.Infrastructure.csproj services/Ledger/src/Fintrack.Ledger.Infrastructure/
COPY services/Ledger/src/Fintrack.Ledger.Application/Fintrack.Ledger.Application.csproj services/Ledger/src/Fintrack.Ledger.Application/
COPY services/Ledger/src/Fintrack.Ledger.Domain/Fintrack.Ledger.Domain.csproj services/Ledger/src/Fintrack.Ledger.Domain/
COPY aspire/Fintrack.ServiceDefaults/Fintrack.ServiceDefaults.csproj aspire/Fintrack.ServiceDefaults/
RUN dotnet restore services/Ledger/src/Fintrack.Ledger.Api/Fintrack.Ledger.Api.csproj

COPY building-blocks/BuildingBlocks.Api/ building-blocks/BuildingBlocks.Api/
COPY building-blocks/BuildingBlocks.Infrastructure/ building-blocks/BuildingBlocks.Infrastructure/
COPY building-blocks/BuildingBlocks.Application/ building-blocks/BuildingBlocks.Application/
COPY building-blocks/BuildingBlocks.Domain/ building-blocks/BuildingBlocks.Domain/
COPY services/Ledger/src/Fintrack.Ledger.Api/ services/Ledger/src/Fintrack.Ledger.Api/
COPY services/Ledger/src/Fintrack.Ledger.Infrastructure/ services/Ledger/src/Fintrack.Ledger.Infrastructure/
COPY services/Ledger/src/Fintrack.Ledger.Application/ services/Ledger/src/Fintrack.Ledger.Application/
COPY services/Ledger/src/Fintrack.Ledger.Domain/ services/Ledger/src/Fintrack.Ledger.Domain/
COPY aspire/Fintrack.ServiceDefaults/ aspire/Fintrack.ServiceDefaults/

WORKDIR /src/services/Ledger/src/Fintrack.Ledger.Api
RUN dotnet build Fintrack.Ledger.Api.csproj -c "$BUILD_CONFIGURATION" --no-restore -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Fintrack.Ledger.Api.csproj \
    -c "$BUILD_CONFIGURATION" \
    --no-restore \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --chown=root:root --chmod=555 --from=publish /app/publish ./
USER nonroot
ENTRYPOINT ["dotnet", "Fintrack.Ledger.Api.dll"]
