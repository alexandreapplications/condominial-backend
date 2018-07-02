FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Presentation/AlexandreApps.Condominial.Backend.UI/AlexandreApps.Condominial.Backend.UI.csproj Presentation/AlexandreApps.Condominial.Backend.UI/
RUN dotnet restore Presentation/AlexandreApps.Condominial.Backend.UI/AlexandreApps.Condominial.Backend.UI.csproj
COPY . .
WORKDIR /src/Presentation/AlexandreApps.Condominial.Backend.UI
RUN dotnet build AlexandreApps.Condominial.Backend.UI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AlexandreApps.Condominial.Backend.UI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AlexandreApps.Condominial.Backend.UI.dll"]
