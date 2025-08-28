# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AuthService.Migrator/AuthService.Migrator.csproj", "AuthService.Migrator/"]
COPY ["AuthService.Infrastructure/AuthService.Infrastructure.csproj", "AuthService.Infrastructure/"]
COPY ["AuthService.Application/AuthService.Application.csproj", "AuthService.Application/"]
COPY ["AuthService.Domain/AuthService.Domain.csproj", "AuthService.Domain/"]

RUN dotnet restore "AuthService.Migrator/AuthService.Migrator.csproj"

COPY . .
WORKDIR "/src/AuthService.Migrator"

RUN dotnet publish "./AuthService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Этап финального образа
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AuthService.Migrator.dll"]
