#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/CloudGamingSupport/BackgroundTask/CloudGamingSupport.BackgroundTask.Updater/CloudGamingSupport.BackgroundTask.Updater.csproj", "Services/CloudGamingSupport/BackgroundTask/CloudGamingSupport.BackgroundTask.Updater/"]
RUN dotnet restore "Services/CloudGamingSupport/BackgroundTask/CloudGamingSupport.BackgroundTask.Updater/CloudGamingSupport.BackgroundTask.Updater.csproj"
COPY . .
WORKDIR "/src/Services/CloudGamingSupport/BackgroundTask/CloudGamingSupport.BackgroundTask.Updater"
RUN dotnet build "CloudGamingSupport.BackgroundTask.Updater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudGamingSupport.BackgroundTask.Updater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudGamingSupport.BackgroundTask.Updater.dll"]