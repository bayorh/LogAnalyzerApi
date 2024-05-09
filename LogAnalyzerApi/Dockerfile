



# Use the official .NET 6 SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Set environment variable for development
ENV ASPNETCORE_ENVIRONMENT=Development

# Copy the project file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the remaining files and build the application
COPY . .
RUN dotnet publish -c Release -o out

# Final stage / create the final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "LogAnalyzerApi.dll"]