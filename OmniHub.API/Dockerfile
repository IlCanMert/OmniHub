# 1. İnşa Aşaması (Build)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Proje dosyalarını kopyalayıp bağımlılıkları indiriyoruz
COPY ["OmniHub.API/OmniHub.API.csproj", "OmniHub.API/"]
COPY ["OmniHub.Application/OmniHub.Application.csproj", "OmniHub.Application/"]
COPY ["OmniHub.Infrastructure/OmniHub.Infrastructure.csproj", "OmniHub.Infrastructure/"]
COPY ["OmniHub.Domain/OmniHub.Domain.csproj", "OmniHub.Domain/"]
RUN dotnet restore "OmniHub.API/OmniHub.API.csproj"

# Tüm kodları kopyalayıp derliyoruz
COPY . .
WORKDIR "/src/OmniHub.API"
RUN dotnet publish "OmniHub.API.csproj" -c Release -o /app/publish

# 2. Çalıştırma Aşaması (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OmniHub.API.dll"]