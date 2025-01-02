FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ostwest_Shop.Server/Ostwest_Shop.Server.csproj", "Ostwest_Shop.Server/"]
COPY ["ostwest_shop.client/ostwest_shop.client.esproj", "ostwest_shop.client/"]
RUN dotnet restore "Ostwest_Shop.Server/Ostwest_Shop.Server.csproj"
COPY . .
WORKDIR "/src/Ostwest_Shop.Server"
RUN dotnet build "Ostwest_Shop.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ostwest_Shop.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ostwest_Shop.Server.dll"] 
