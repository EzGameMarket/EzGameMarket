#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Review/Review.API/Review.API.csproj", "Services/Review/Review.API/"]
RUN dotnet restore "Services/Review/Review.API/Review.API.csproj"
COPY . .
WORKDIR "/src/Services/Review/Review.API"
RUN dotnet build "Review.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Review.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Review.API.dll"]