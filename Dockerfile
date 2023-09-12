# Base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Koton.Staj.Northwind.WebAPI/Koton.Staj.Northwind.WebAPI.csproj", "WebAPI/"]
RUN dotnet restore "WebAPI/Koton.Staj.Northwind.WebAPI.csproj"
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "Koton.Staj.Northwind.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Koton.Staj.Northwind.WebAPI.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Koton.Staj.Northwind.WebAPI.dll"]
