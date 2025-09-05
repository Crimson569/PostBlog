FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PostService.Migrator/PostService.Migrator.csproj", "PostService.Migrator/"]
COPY ["PostService.Infrastructure/PostService.Infrastructure.csproj", "PostService.Infrastructure/"]
COPY ["PostService.Application/PostService.Application.csproj", "PostService.Application/"]
COPY ["PostService.Domain/PostService.Domain.csproj", "PostService.Domain/"]
COPY ["AuthService.Domain/AuthService.Domain.csproj", "AuthService.Domain/"]
COPY ["AuthService.Application/AuthService.Application.csproj", "AuthService.Application/"]
RUN dotnet restore "PostService.Migrator/PostService.Migrator.csproj"
COPY . .
WORKDIR "/src/PostService.Migrator"
RUN dotnet build "./PostService.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PostService.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostService.Migrator.dll"]
