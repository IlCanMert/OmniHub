FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["OmniHub.Client/OmniHub.Client.csproj", "OmniHub.Client/"]
COPY ["OmniHub.Application/OmniHub.Application.csproj", "OmniHub.Application/"]
COPY ["OmniHub.Domain/OmniHub.Domain.csproj", "OmniHub.Domain/"]
RUN dotnet restore "OmniHub.Client/OmniHub.Client.csproj"
COPY . .
WORKDIR "/src/OmniHub.Client"
RUN dotnet publish "OmniHub.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .

RUN sed -i 's/blazor.webassembly#\[.{fingerprint}\].js/blazor.webassembly.js/g' index.html || true

RUN echo "events { } http { include mime.types; types { application/wasm wasm; } server { listen 80; location / { root /usr/share/nginx/html; try_files \$uri \$uri/ /index.html =404; } } }" > /etc/nginx/nginx.conf

EXPOSE 80