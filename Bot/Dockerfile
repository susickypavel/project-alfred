FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["project-alfred/project-alfred.csproj", "project-alfred/"]
RUN dotnet restore "project-alfred/project-alfred.csproj"
COPY . .
WORKDIR "/src/project-alfred"
RUN dotnet build "project-alfred.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "project-alfred.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "project-alfred.dll"]
