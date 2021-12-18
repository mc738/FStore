FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FStore.App/FStore.App.fsproj", "FStore.App/"]
RUN dotnet restore "FStore.App/FStore.App.fsproj"
COPY . .
WORKDIR "/src/FStore.App"
RUN dotnet build "FStore.App.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FStore.App.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FStore.App.dll"]
