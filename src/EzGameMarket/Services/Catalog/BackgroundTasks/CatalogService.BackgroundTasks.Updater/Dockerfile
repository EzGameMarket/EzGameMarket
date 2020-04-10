#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Catalog/BackgroundTasks/CatalogService.BackgroundTasks.Updater/CatalogService.BackgroundTasks.Updater.csproj", "Services/Catalog/BackgroundTasks/CatalogService.BackgroundTasks.Updater/"]
RUN dotnet restore "Services/Catalog/BackgroundTasks/CatalogService.BackgroundTasks.Updater/CatalogService.BackgroundTasks.Updater.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/BackgroundTasks/CatalogService.BackgroundTasks.Updater"
RUN dotnet build "CatalogService.BackgroundTasks.Updater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatalogService.BackgroundTasks.Updater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogService.BackgroundTasks.Updater.dll"]