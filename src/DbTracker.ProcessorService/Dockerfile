FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/DbTracker.ProcessorService/DbTracker.ProcessorService.csproj", "DbTracker.ProcessorService/"]
COPY ["src/DbTracker.Shared/DbTracker.Shared.csproj", "DbTracker.Shared/"]
RUN dotnet restore "DbTracker.ProcessorService/DbTracker.ProcessorService.csproj"
COPY src/ .
WORKDIR "/src/DbTracker.ProcessorService"
RUN dotnet build "DbTracker.ProcessorService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DbTracker.ProcessorService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DbTracker.ProcessorService.dll"] 