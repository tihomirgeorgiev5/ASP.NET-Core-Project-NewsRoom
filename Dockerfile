# base image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

# build
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["NewsRoom/NewsRoom.csproj", "NewsRoom/"]
RUN dotnet restore "NewsRoom/NewsRoom.csproj"
COPY . .
WORKDIR "/src/NewsRoom"
RUN dotnet build "NewsRoom.csproj" -c Release -o /app/build

# publish
FROM build AS publish
RUN dotnet publish "NewsRoom.csproj" -c Release -o /app/publish

# final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsRoom.dll"]
