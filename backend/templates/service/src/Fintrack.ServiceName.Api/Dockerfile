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
COPY services/ServiceName/src/Fintrack.ServiceName.Api/Fintrack.ServiceName.Api.csproj services/ServiceName/src/Fintrack.ServiceName.Api/
COPY services/ServiceName/src/Fintrack.ServiceName.Infrastructure/Fintrack.ServiceName.Infrastructure.csproj services/ServiceName/src/Fintrack.ServiceName.Infrastructure/
COPY services/ServiceName/src/Fintrack.ServiceName.Application/Fintrack.ServiceName.Application.csproj services/ServiceName/src/Fintrack.ServiceName.Application/
COPY services/ServiceName/src/Fintrack.ServiceName.Domain/Fintrack.ServiceName.Domain.csproj services/ServiceName/src/Fintrack.ServiceName.Domain/
COPY aspire/Fintrack.ServiceDefaults/Fintrack.ServiceDefaults.csproj aspire/Fintrack.ServiceDefaults/
RUN dotnet restore services/ServiceName/src/Fintrack.ServiceName.Api/Fintrack.ServiceName.Api.csproj

COPY services/ServiceName/src/Fintrack.ServiceName.Api/ services/ServiceName/src/Fintrack.ServiceName.Api/
COPY services/ServiceName/src/Fintrack.ServiceName.Infrastructure/ services/ServiceName/src/Fintrack.ServiceName.Infrastructure/
COPY services/ServiceName/src/Fintrack.ServiceName.Application/ services/ServiceName/src/Fintrack.ServiceName.Application/
COPY services/ServiceName/src/Fintrack.ServiceName.Domain/ services/ServiceName/src/Fintrack.ServiceName.Domain/
COPY aspire/Fintrack.ServiceDefaults/ aspire/Fintrack.ServiceDefaults/

WORKDIR /src/services/ServiceName/src/Fintrack.ServiceName.Api
RUN dotnet build Fintrack.ServiceName.Api.csproj -c "$BUILD_CONFIGURATION" --no-restore -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Fintrack.ServiceName.Api.csproj -c "$BUILD_CONFIGURATION" --no-restore -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --chown=nonroot:nonroot --from=publish /app/publish ./
USER nonroot
ENTRYPOINT ["dotnet", "Fintrack.ServiceName.Api.dll"]
