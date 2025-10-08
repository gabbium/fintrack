FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

ARG APP_UID=64123
ARG APP_GID=64123
RUN groupadd --gid ${APP_GID} nonroot \
    && useradd --uid ${APP_UID} --gid ${APP_GID} --create-home --shell /usr/sbin/nologin nonroot

WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Directory.Build.props ./
COPY Directory.Packages.props ./
COPY services/Planning/src/Fintrack.Planning.Api/Fintrack.Planning.Api.csproj services/Planning/src/Fintrack.Planning.Api/
COPY services/Planning/src/Fintrack.Planning.Infrastructure/Fintrack.Planning.Infrastructure.csproj services/Planning/src/Fintrack.Planning.Infrastructure/
COPY services/Planning/src/Fintrack.Planning.Application/Fintrack.Planning.Application.csproj services/Planning/src/Fintrack.Planning.Application/
COPY services/Planning/src/Fintrack.Planning.Domain/Fintrack.Planning.Domain.csproj services/Planning/src/Fintrack.Planning.Domain/
COPY aspire/Fintrack.ServiceDefaults/Fintrack.ServiceDefaults.csproj aspire/Fintrack.ServiceDefaults/

RUN dotnet restore services/Planning/src/Fintrack.Planning.Api/Fintrack.Planning.Api.csproj

COPY services/Planning/src/Fintrack.Planning.Api/ services/Planning/src/Fintrack.Planning.Api/
COPY services/Planning/src/Fintrack.Planning.Infrastructure/ services/Planning/src/Fintrack.Planning.Infrastructure/
COPY services/Planning/src/Fintrack.Planning.Application/ services/Planning/src/Fintrack.Planning.Application/
COPY services/Planning/src/Fintrack.Planning.Domain/ services/Planning/src/Fintrack.Planning.Domain/
COPY aspire/Fintrack.ServiceDefaults/ aspire/Fintrack.ServiceDefaults/

WORKDIR /src/services/Planning/src/Fintrack.Planning.Api
RUN dotnet build Fintrack.Planning.Api.csproj -c "$BUILD_CONFIGURATION" --no-restore -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Fintrack.Planning.Api.csproj -c "$BUILD_CONFIGURATION" --no-restore -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --chown=nonroot:nonroot --from=publish /app/publish ./
USER nonroot
ENTRYPOINT ["dotnet", "Fintrack.Planning.Api.dll"]
