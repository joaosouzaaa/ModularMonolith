FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/ModularMonolith.API/ModularMonolith.API.csproj", "src/API/ModularMonolith.API/"]
COPY ["src/Appointment/Appointment.DependencyInjection/Appointment.DependencyInjection.csproj", "src/Appointment/Appointment.DependencyInjection/"]
COPY ["src/Appointment/Appointment.ApplicationService/Appointment.ApplicationService.csproj", "src/Appointment/Appointment.ApplicationService/"]
COPY ["src/Common/ModularMonolith.Common/ModularMonolith.Common.csproj", "src/Common/ModularMonolith.Common/"]
COPY ["src/Appointment/Appointment.Domain/Appointment.Domain.csproj", "src/Appointment/Appointment.Domain/"]
COPY ["src/Appointment/Appointment.Infrastructure/Appointment.Infrastructure.csproj", "src/Appointment/Appointment.Infrastructure/"]
COPY ["src/Doctor/Doctor.DependencyInjection/Doctor.DependencyInjection.csproj", "src/Doctor/Doctor.DependencyInjection/"]
COPY ["src/Doctor/Doctor.ApplicationService/Doctor.ApplicationService.csproj", "src/Doctor/Doctor.ApplicationService/"]
COPY ["src/Doctor/Doctor.Domain/Doctor.Domain.csproj", "src/Doctor/Doctor.Domain/"]
COPY ["src/Doctor/Doctor.Infrastructure/Doctor.Infrastructure.csproj", "src/Doctor/Doctor.Infrastructure/"]
COPY ["src/Patient/Patient.DependencyInjection/Patient.DependencyInjection.csproj", "src/Patient/Patient.DependencyInjection/"]
COPY ["src/Patient/Patient.ApplicationServices/Patient.ApplicationServices.csproj", "src/Patient/Patient.ApplicationServices/"]
COPY ["src/Patient/Patient.Domain/Patient.Domain.csproj", "src/Patient/Patient.Domain/"]
COPY ["src/Patient/Patient.Infrastructure/Patient.Infrastructure.csproj", "src/Patient/Patient.Infrastructure/"]
RUN dotnet restore "./src/API/ModularMonolith.API/ModularMonolith.API.csproj"
COPY . .
WORKDIR "/src/src/API/ModularMonolith.API"
RUN dotnet build "./ModularMonolith.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ModularMonolith.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModularMonolith.API.dll"]