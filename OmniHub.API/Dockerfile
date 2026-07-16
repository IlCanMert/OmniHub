# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["OmniHub.API/OmniHub.API.csproj", "OmniHub.API/"]
COPY ["OmniHub.Application/OmniHub.Application.csproj", "OmniHub.Application/"]
COPY ["OmniHub.Infrastructure/OmniHub.Infrastructure.csproj", "OmniHub.Infrastructure/"]
COPY ["OmniHub.Domain/OmniHub.Domain.csproj", "OmniHub.Domain/"]
RUN dotnet restore "OmniHub.API/OmniHub.API.csproj"

# Copy all source code and publish
COPY . .
WORKDIR "/src/OmniHub.API"
RUN dotnet publish "OmniHub.API.csproj" -c Release -o /app/publish

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OmniHub.API.dll"]