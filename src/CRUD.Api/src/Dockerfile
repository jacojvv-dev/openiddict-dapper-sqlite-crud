FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CRUD.Api.Api/CRUD.Api.Api.csproj", "CRUD.Api.Api/"]
RUN dotnet restore "CRUD.Api.Api/CRUD.Api.Api.csproj"
COPY . .
WORKDIR "/src/CRUD.Api.Api"
RUN dotnet build "CRUD.Api.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CRUD.Api.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRUD.Api.Api.dll"]
