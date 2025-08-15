FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["queue-service/QueueService.csproj", "queue-service/"]
RUN dotnet restore "queue-service/QueueService.csproj"

COPY . .
WORKDIR "/src/queue-service"
RUN dotnet build "QueueService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QueueService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueueService.dll"]
