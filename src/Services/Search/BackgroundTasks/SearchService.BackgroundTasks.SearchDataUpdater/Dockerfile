#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Search/BackgroundTasks/SearchService.BackgroundTasks.SearchDataUpdater/SearchService.BackgroundTasks.SearchDataUpdater.csproj", "Services/Search/BackgroundTasks/SearchService.BackgroundTasks.SearchDataUpdater/"]
RUN dotnet restore "Services/Search/BackgroundTasks/SearchService.BackgroundTasks.SearchDataUpdater/SearchService.BackgroundTasks.SearchDataUpdater.csproj"
COPY . .
WORKDIR "/src/Services/Search/BackgroundTasks/SearchService.BackgroundTasks.SearchDataUpdater"
RUN dotnet build "SearchService.BackgroundTasks.SearchDataUpdater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SearchService.BackgroundTasks.SearchDataUpdater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SearchService.BackgroundTasks.SearchDataUpdater.dll"]