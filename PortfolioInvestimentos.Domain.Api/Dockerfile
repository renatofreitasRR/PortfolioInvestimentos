#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PortfolioInvestimentos.Domain.Api/PortfolioInvestimentos.Domain.Api.csproj", "PortfolioInvestimentos.Domain.Api/"]
COPY ["PortfolioInvestimentos.Domain.Infra/PortfolioInvestimentoss.Domain.Infra.csproj", "PortfolioInvestimentoss.Domain.Infra/"]
COPY ["PortfolioInvestimentos.Domain/PortfolioInvestimentoss.Domain.csproj", "PortfolioInvestimentoss.Domain/"]
RUN dotnet restore "./PortfolioInvestimentos.Domain.Api/./PortfolioInvestimentos.Domain.Api.csproj"
COPY . .
WORKDIR "/src/PortfolioInvestimentos.Domain.Api"
RUN dotnet build "./PortfolioInvestimentos.Domain.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PortfolioInvestimentos.Domain.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortfolioInvestimentos.Domain.Api.dll"]