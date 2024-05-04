FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CinemaTicketing.API/CinemaTicketing.API.csproj", "CinemaTicketing.API/"]
COPY ["CinemaTicketing.Infrastructure/CinemaTicketing.Infrastructure.csproj", "CinemaTicketing.Infrastructure/"]
COPY ["CinemaTicketing.Application/CinemaTicketing.Application.csproj", "CinemaTicketing.Application/"]
COPY ["CinemaTicketing.Domain/CinemaTicketing.Domain.csproj", "CinemaTicketing.Domain/"]
COPY ["CinemaTicketing.Contracts/CinemaTicketing.Contracts.csproj", "CinemaTicketing.Contracts/"]
RUN dotnet restore "CinemaTicketing.API/CinemaTicketing.API.csproj"
COPY . .
WORKDIR "/src/CinemaTicketing.API"
RUN dotnet build "CinemaTicketing.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CinemaTicketing.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -la
COPY ["cert.pfx", "cert.pfx"]
ENTRYPOINT ["dotnet", "CinemaTicketing.API.dll"]
