#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Koton.Staj.Northwind.WebAPI/Koton.Staj.Northwind.WebAPI.csproj", "Koton.Staj.Northwind.WebAPI/"]
COPY ["Koton.Staj.Northwind.Business/Koton.Staj.Northwind.Business.csproj", "Koton.Staj.Northwind.Business/"]
COPY ["Koton.Staj.Northwind.Data/Koton.Staj.Northwind.Data.csproj", "Koton.Staj.Northwind.Data/"]
COPY ["Koton.Staj.Northwind.Entities/Koton.Staj.Northwind.Entities.csproj", "Koton.Staj.Northwind.Entities/"]
RUN dotnet restore "Koton.Staj.Northwind.WebAPI/Koton.Staj.Northwind.WebAPI.csproj"
COPY . .
WORKDIR "/src/Koton.Staj.Northwind.WebAPI"
RUN dotnet build "Koton.Staj.Northwind.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Koton.Staj.Northwind.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Koton.Staj.Northwind.WebAPI.dll"]