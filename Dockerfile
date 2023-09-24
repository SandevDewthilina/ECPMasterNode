FROM node:18 as npmstage
COPY ["ECPMaster/package.json", "/code/"]
WORKDIR /code
RUN npm install

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ECPMaster/ECPMaster.csproj", "ECPMaster/"]
COPY --from=npmstage /code/node_modules ECPMaster/
RUN dotnet restore "ECPMaster/ECPMaster.csproj"
COPY . .
WORKDIR "/src/ECPMaster"
RUN dotnet build "ECPMaster.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECPMaster.csproj" -c Release -o /app/publish

# Switch to the publish directory and run npm install
WORKDIR /app/publish
RUN npm install

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECPMaster.dll"]
