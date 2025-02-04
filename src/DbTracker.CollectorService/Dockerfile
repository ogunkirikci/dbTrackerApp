FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/DbTracker.CollectorService/DbTracker.CollectorService.csproj", "DbTracker.CollectorService/"]
COPY ["src/DbTracker.Shared/DbTracker.Shared.csproj", "DbTracker.Shared/"]
RUN dotnet restore "DbTracker.CollectorService/DbTracker.CollectorService.csproj"
COPY src/ .
WORKDIR "/src/DbTracker.CollectorService"
RUN dotnet build "DbTracker.CollectorService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DbTracker.CollectorService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DbTracker.CollectorService.dll"] 