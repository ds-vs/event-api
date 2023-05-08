FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_URLS=https://*:443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Event.API/Event.API.csproj", "Event.API/"]
COPY ["Event.DAL/Event.DAL.csproj", "Event.DAL/"]
COPY ["Event.Service/Event.Service.csproj", "Event.Service/"]
COPY ["Event.Domain/Event.Domain.csproj", "Event.Domain/"]
RUN dotnet restore "Event.API/Event.API.csproj"
COPY . .
WORKDIR "/src/Event.API"
RUN dotnet build "Event.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Event.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Event.API.dll"]