FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CRUD.Identity.Web/CRUD.Identity.Web.csproj", "CRUD.Identity.Web/"]
RUN dotnet restore "CRUD.Identity.Web/CRUD.Identity.Web.csproj"
COPY . .
WORKDIR "/src/CRUD.Identity.Web"
RUN dotnet build "CRUD.Identity.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CRUD.Identity.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRUD.Identity.Web.dll"]
