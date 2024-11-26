# FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build-env
# WORKDIR /App

# # Copy everything
# COPY ["WebShop/WebShop.csproj", "./WebShop/"]
# COPY ["WebShop.Shared/WebShop.Shared.csproj", "./WebShop.Shared/"]
# COPY ["WebShop.DataAccess/WebShop.DataAccess.csproj", "./WebShop.DataAccess/"]
# COPY ["WebShop.Tests/WebShop.Tests.csproj", "./WebShop.Tests/"]


# # Restore dependencies
# RUN dotnet restore "WebShop/WebShop.csproj"

# # Copy the full solution
# COPY . .

# # Build all projects
# RUN dotnet build "WebShop/WebShop.csproj" -c Release

# # Stage 2: Run tests
# FROM build AS test
# WORKDIR /src/WebShop.Tests

# # Run tests and ensure they pass
# RUN dotnet test "WebShop.Tests.csproj" --no-build --logger:trx --results-directory /src/TestResults

# # Stage 3: Publish WebShop (only proceeds if tests pass)
# FROM build AS publish
# WORKDIR /src/WebShop
# RUN dotnet publish "WebShop.csproj" -c Release -o /app/publish /p:UseAppHost=false

# # Stage 4: Final runtime image for WebShop
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "WebShop.dll"]

# Stage 1: Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build
WORKDIR /App

# Copy project files
COPY ["WebShop/WebShop.csproj", "./WebShop/"]
COPY ["WebShop.Shared/WebShop.Shared.csproj", "./WebShop.Shared/"]
COPY ["WebShop.DataAccess/WebShop.DataAccess.csproj", "./WebShop.DataAccess/"]
COPY ["WebShop.Tests/WebShop.Tests.csproj", "./WebShop.Tests/"]

# Restore dependencies
RUN dotnet restore "WebShop/WebShop.csproj"

# Copy full solution
COPY . .

# Build solution
RUN dotnet build "WebShop/WebShop.csproj" -c Release

# Stage 2: Run Tests
FROM build AS test
WORKDIR /App/WebShop.Tests
RUN dotnet test "WebShop.Tests.csproj" --no-build --logger:trx --results-directory /App/TestResults

# Stage 3: Publish WebShop
FROM build AS publish
WORKDIR /App/WebShop
RUN dotnet publish "WebShop.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebShop.dll"]