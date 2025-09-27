# Use official .NET 6 SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy project files
COPY ["OyoAgro.Api/OyoAgro.Api.csproj", "OyoAgro.Api/"]
COPY ["OyoAgro.BusinessLogic.Layer/OyoAgro.BusinessLogic.Layer.csproj", "OyoAgro.BusinessLogic.Layer/"]
COPY ["OyoAgro.DataAccess.Layer/OyoAgro.DataAccess.Layer.csproj", "OyoAgro.DataAccess.Layer/"]

# Restore dependencies
RUN dotnet restore "OyoAgro.Api/OyoAgro.Api.csproj"

# Copy everything else
COPY . .

# Build and publish API
WORKDIR "/src/OyoAgro.Api"
RUN dotnet publish -c Release -o /app/publish

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OyoAgro.Api.dll"]
