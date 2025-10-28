FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["RisedorApi.Api/RisedorApi.Api.csproj", "RisedorApi.Api/"]
COPY ["RisedorApi.Application/RisedorApi.Application.csproj", "RisedorApi.Application/"]
COPY ["RisedorApi.Domain/RisedorApi.Domain.csproj", "RisedorApi.Domain/"]
COPY ["RisedorApi.Infrastructure/RisedorApi.Infrastructure.csproj", "RisedorApi.Infrastructure/"]
COPY ["RisedorApi.Shared/RisedorApi.Shared.csproj", "RisedorApi.Shared/"]
RUN dotnet restore "RisedorApi.Api/RisedorApi.Api.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "RisedorApi.Api/RisedorApi.Api.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Render uses PORT environment variable
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE $PORT

ENTRYPOINT ["dotnet", "RisedorApi.Api.dll"]