FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ClockIn-ClockOut.csproj ./
RUN dotnet restore "./ClockIn-ClockOut.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ClockIn-ClockOut.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ClockIn-ClockOut.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD dotnet ClockIn-ClockOut.dll