FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/ModularMonolith.API/ModularMonolith.API.csproj", "src/API/ModularMonolith.API/"]
COPY ["src/Modules/Appointment/Appointment.DependencyInjection/Appointment.DependencyInjection.csproj", "src/Modules/Appointment/Appointment.DependencyInjection/"]
COPY ["src/Modules/Appointment/Appointment.ApplicationService/Appointment.ApplicationService.csproj", "src/Modules/Appointment/Appointment.ApplicationService/"]
COPY ["src/Common/ModularMonolith.Common/ModularMonolith.Common.csproj", "src/Common/ModularMonolith.Common/"]
COPY ["src/Modules/Appointment/Appointment.Domain/Appointment.Domain.csproj", "src/Modules/Appointment/Appointment.Domain/"]
COPY ["src/Modules/Appointment/Appointment.Infrastructure/Appointment.Infrastructure.csproj", "src/Modules/Appointment/Appointment.Infrastructure/"]
COPY ["src/Modules/Doctor/Doctor.DependencyInjection/Doctor.DependencyInjection.csproj", "src/Modules/Doctor/Doctor.DependencyInjection/"]
COPY ["src/Modules/Doctor/Doctor.ApplicationService/Doctor.ApplicationService.csproj", "src/Modules/Doctor/Doctor.ApplicationService/"]
COPY ["src/Modules/Doctor/Doctor.Domain/Doctor.Domain.csproj", "src/Modules/Doctor/Doctor.Domain/"]
COPY ["src/Modules/Doctor/Doctor.Infrastructure/Doctor.Infrastructure.csproj", "src/Modules/Doctor/Doctor.Infrastructure/"]
COPY ["src/Modules/Patient/Patient.DependencyInjection/Patient.DependencyInjection.csproj", "src/Modules/Patient/Patient.DependencyInjection/"]
COPY ["src/Modules/Patient/Patient.ApplicationServices/Patient.ApplicationServices.csproj", "src/Modules/Patient/Patient.ApplicationServices/"]
COPY ["src/Modules/Patient/Patient.Domain/Patient.Domain.csproj", "src/Modules/Patient/Patient.Domain/"]
COPY ["src/Modules/Patient/Patient.Infrastructure/Patient.Infrastructure.csproj", "src/Modules/Patient/Patient.Infrastructure/"]
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