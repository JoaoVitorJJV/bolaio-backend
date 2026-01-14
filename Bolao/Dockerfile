FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["UFRA.Bolao.API/UFRA.Bolao.API.csproj", "UFRA.Bolao.API/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]

RUN dotnet restore "UFRA.Bolao.API/UFRA.Bolao.API.csproj"

COPY . .

WORKDIR "/src/UFRA.Bolao.API"
RUN dotnet build "UFRA.Bolao.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UFRA.Bolao.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "UFRA.Bolao.API.dll"]