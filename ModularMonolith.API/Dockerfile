#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ModularMonolith.API/ModularMonolith.API.csproj", "ModularMonolith.API/"]
COPY ["Doctor.DependencyInjection/Doctor.DependencyInjection.csproj", "Doctor.DependencyInjection/"]
COPY ["Doctor.ApplicationService/Doctor.ApplicationService.csproj", "Doctor.ApplicationService/"]
COPY ["Doctor.Infrastructure/Doctor.Infrastructure.csproj", "Doctor.Infrastructure/"]
COPY ["Doctor.Domain/Doctor.Domain.csproj", "Doctor.Domain/"]
COPY ["ModularMonolith.Common/ModularMonolith.Common.csproj", "ModularMonolith.Common/"]
COPY ["Patient.DependencyInjection/Patient.DependencyInjection.csproj", "Patient.DependencyInjection/"]
COPY ["Patient.ApplicationServices/Patient.ApplicationServices.csproj", "Patient.ApplicationServices/"]
COPY ["Patient.Infrastructure/Patient.Infrastructure.csproj", "Patient.Infrastructure/"]
COPY ["Patient.Domain/Patient.Domain.csproj", "Patient.Domain/"]
RUN dotnet restore "./ModularMonolith.API/./ModularMonolith.API.csproj"
COPY . .
WORKDIR "/src/ModularMonolith.API"
RUN dotnet build "./ModularMonolith.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ModularMonolith.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModularMonolith.API.dll"]