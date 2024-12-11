FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MotoRentalApp.Api/MotoRentalApp.Api.csproj", "MotoRentalApp.Api/"]
COPY ["MotoRentalApp.Application/MotoRentalApp.Application.csproj", "MotoRentalApp.Application/"]
COPY ["MotoRentalApp.Domain/MotoRentalApp.Domain.csproj", "MotoRentalApp.Domain/"]
COPY ["MotoRentalApp.Infrastructure/MotoRentalApp.Infrastructure.csproj", "MotoRentalApp.Infrastructure/"]
RUN dotnet restore "MotoRentalApp.Api/MotoRentalApp.Api.csproj"

COPY . .
WORKDIR "/src/MotoRentalApp.Api"
RUN dotnet build "MotoRentalApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MotoRentalApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotoRentalApp.Api.dll"]
