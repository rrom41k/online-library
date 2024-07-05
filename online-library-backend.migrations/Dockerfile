FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["online-library-backend.migrations/online-library-backend.migrations.csproj", "online-library-backend.migrations/"]
COPY ["online-library-backend/online-library-backend.csproj", "online-library-backend/"]
RUN dotnet restore "online-library-backend.migrations/online-library-backend.migrations.csproj"
WORKDIR "/src/online-library-backend.migrations"
COPY . .
RUN dotnet build "online-library-backend.migrations.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "online-library-backend.migrations.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "online-library-backend.migrations.dll"]
