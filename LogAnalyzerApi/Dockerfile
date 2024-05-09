
# Use the official .NET 6 SDK as a build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Set environment variables for the build stage
ENV ASPNETCORE_ENVIRONMENT=Development


# Copy the project file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the remaining source code
COPY . .


# Build the application
RUN dotnet publish -c Release -o out


# Use the official .NET 6 runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Set environment variables for the runtime stage
ENV ASPNETCORE_ENVIRONMENT=Development

# Copy the published output from the build image
COPY --from=build /app/out .

# Expose the port your app will run on
EXPOSE 80

# Define the entry point for your application
ENTRYPOINT ["dotnet", "LogAnalyzerApi.dll"]