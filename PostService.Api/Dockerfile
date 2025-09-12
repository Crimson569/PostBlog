FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PostService.Api/PostService.Api.csproj", "PostService.Api/"]
COPY ["PostService.Application/PostService.Application.csproj", "PostService.Application/"]
COPY ["PostService.Domain/PostService.Domain.csproj", "PostService.Domain/"]
COPY ["PostService.Infrastructure/PostService.Infrastructure.csproj", "PostService.Infrastructure/"]
COPY ["AuthService.Application/AuthService.Application.csproj", "AuthService.Application/"]
COPY ["AuthService.Domain/AuthService.Domain.csproj", "AuthService.Domain/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "PostService.Api/PostService.Api.csproj"
COPY . .
WORKDIR "/src/PostService.Api"
RUN dotnet build "./PostService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PostService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostService.Api.dll"]
