#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Ids4/Ids4.csproj", "Ids4/"]
COPY ["nuget.config", "/src"]
RUN dotnet restore "Ids4/Ids4.csproj"
COPY . .
WORKDIR "/src/Ids4"
RUN dotnet build "Ids4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ids4.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ids4.dll"]