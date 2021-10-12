#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WepApp/WepApp.csproj", "WepApp/"]
COPY ["UseCases/UseCases.csproj", "UseCases/"]
COPY ["Infrastructure.Interfaces/Infrastructure.Interfaces.csproj", "Infrastructure.Interfaces/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Utils/Utils.csproj", "Utils/"]
COPY ["Infrastructure.Implemtation/Infrastructure.Implemtation.csproj", "Infrastructure.Implemtation/"]
COPY . .
RUN dotnet restore "WepApp/WepApp.csproj"
WORKDIR "/src/WepApp"
RUN dotnet build "WepApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WepApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WepApp.dll"]