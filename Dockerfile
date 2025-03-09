# Stage 1: Build the .NET application
FROM sandevdewthilina/ecp-core:base-image-3.1-asp.net-core AS dotnet-build
WORKDIR /master

# Copy the solution and project files
COPY *.sln .
COPY ECPMaster/*.csproj ./ECPMaster/

# Restore .NET dependencies
RUN dotnet restore

# Copy the remaining source code
COPY ECPMaster/ ./ECPMaster/

# Publish the .NET application
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Install Node.js dependencies
FROM node:18 AS node-build
WORKDIR /app

# Copy package.json and package-lock.json
COPY ECPMaster/package*.json ./

# Install Node.js dependencies
RUN npm install

# Stage 3: Create the runtime image
FROM sandevdewthilina/ecp-core:base-runtime-3.1-asp.net-core AS runtime
WORKDIR /app

# Copy the published .NET application
COPY --from=dotnet-build /app/publish .

# Copy the node_modules directory
COPY --from=node-build /app/node_modules ./node_modules

EXPOSE 18001
EXPOSE 3307
ENTRYPOINT ["dotnet", "ECPMaster.dll"]
