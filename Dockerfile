FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet publish "WebApiDemo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
RUN mkdir notes
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebApiDemo.dll"]