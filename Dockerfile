FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CV-project/CV-project.csproj", "CV-project/"]
RUN dotnet restore "CV-project/CV-project.csproj"
COPY . .
WORKDIR "/src/CV-project"
RUN dotnet build "CV-project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CV-project.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "CV-project.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CV-project.dll