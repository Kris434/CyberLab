
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["CyberLab_backend/CyberLab_backend.csproj", "CyberLab_backend/"]
COPY ["CyberLab.Domain/CyberLab.Domain.csproj", "CyberLab.Domain/"]
COPY ["CyberLab.Application/CyberLab.Application.csproj", "CyberLab.Application/"]
COPY ["CyberLab.Infrastructure/CyberLab.Infrastructure.csproj", "CyberLab.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "CyberLab_backend/CyberLab_backend.csproj"

# Copy source files
COPY . .

# Build
WORKDIR /src/CyberLab_backend
RUN dotnet build "CyberLab_backend.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build \
    --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CyberLab_backend.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CyberLab_backend.dll"]
