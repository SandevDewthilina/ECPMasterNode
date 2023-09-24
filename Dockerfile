FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ECPMaster/ECPMaster.csproj", "ECPMaster/"]
RUN dotnet restore "ECPMaster/ECPMaster.csproj"
COPY . .
WORKDIR "/src/ECPMaster"
RUN dotnet build "ECPMaster.csproj" -c Release -o /app/build

FROM node:18 AS node
RUN npm install
FROM build AS publish
RUN dotnet publish "ECPMaster.csproj" -c Release -o /app/publish
COPY --from=node /node_modules /app/publish/node_modules/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECPMaster.dll"]
