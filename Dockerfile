FROM mcr.microsoft.com/dotnet/sdk:10.0 as build
WORKDIR /src

COPY ["Orcamentaria.CompanyService.API/Orcamentaria.CompanyService.API.csproj", "Orcamentaria.CompanyService.API/"]
COPY ["Orcamentaria.CompanyService.Application/Orcamentaria.CompanyService.Application.csproj", "Orcamentaria.CompanyService.Application/"]
COPY ["Orcamentaria.CompanyService.Domain/Orcamentaria.CompanyService.Domain.csproj", "Orcamentaria.CompanyService.Domain/"]
COPY ["Orcamentaria.CompanyService.Infrastructure/Orcamentaria.CompanyService.Infrastructure.csproj", "Orcamentaria.CompanyService.Infrastructure/"]

COPY nuget.config ./
COPY local-packages ./local-packages

RUN dotnet restore "Orcamentaria.CompanyService.API/Orcamentaria.CompanyService.API.csproj"

COPY . .

WORKDIR "/src/Orcamentaria.CompanyService.API"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Orcamentaria.CompanyService.API.dll"]
# ENV ASPNETCORE_URLS=http://+:5000
# EXPOSE 5000