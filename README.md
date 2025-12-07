# ECPMasterNode

ECPMasterNode is a .NET Core 3.1 application. This repository contains the source code for the ECP Master Node service. The README provides an overview, quick start, development notes, configuration hints, and contribution guidance.

> Note: This project targets .NET Core 3.1 (netcoreapp3.1). Make sure you have the .NET Core 3.1 SDK installed.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Quickstart](#quickstart)
- [Configuration](#configuration)
- [Development](#development)
- [Testing](#testing)
- [Deploying](#deploying)
- [Contributing](#contributing)
- [License](#license)

## Prerequisites

- .NET Core SDK 3.1 (download: https://dotnet.microsoft.com/download/dotnet/3.1)
- Optional: Docker, if you want to containerize the application
- Any runtime dependencies the project requires (e.g. a database or message broker). See `appsettings.json` or the project docs for details.

## Quickstart

1. Clone the repository:

   git clone https://github.com/SandevDewthilina/ECPMasterNode.git
   cd ECPMasterNode

2. Restore and build:

   dotnet restore
   dotnet build --configuration Release

3. Run the application (from the project folder that contains the .csproj file):

   dotnet run --project ./src/YourProjectName/YourProjectName.csproj

   Or if there is a top-level project in the repo root:

   dotnet run

4. Open the configured port in your browser or API client (check appsettings or launchProfile for the port). By default Kestrel will use ports defined in launchSettings.json / appsettings.

## Configuration

- appsettings.json and appsettings.{Environment}.json are used to configure the application.
- Common configuration items:
  - Connection strings
  - Logging levels
  - External service endpoints
- Sensitive configuration should be provided via environment variables or a secure secret store in production.

Example (environment variable):

  export ConnectionStrings__DefaultConnection="Server=...;Database=...;User Id=...;Password=...;"

## Development

- The project targets .NET Core 3.1 (TargetFramework: netcoreapp3.1). Ensure your IDE supports this (Visual Studio 2019 or later, VS Code with C# extension).
- Useful commands:
  - dotnet restore
  - dotnet build
  - dotnet run
  - dotnet test
- If you add new projects, keep solution references up-to-date and ensure CI (if present) builds the solution.

## Testing

- Unit and integration tests (if present) can be run with:

  dotnet test

- Add test projects next to implementation projects using the `xUnit` or preferred test framework.

## Deploying

- You can publish a release build with:

  dotnet publish -c Release -o ./publish

- Optionally build a Docker image (if a Dockerfile exists):

  docker build -t ecpmasternode:latest .

- Deploy according to your infrastructure (Kubernetes, Docker Compose, VM, etc.).

## Contributing

- Contributions are welcome. Please open an issue to discuss significant changes before sending a pull request.
- Follow these basic steps:
  1. Fork the repo
  2. Create a branch feature/your-feature
  3. Commit your changes with clear messages
  4. Open a PR against the default branch

## License

This repository does not yet specify a license. If you want to use a permissive license, consider adding an MIT license file (LICENSE) at the repository root.

---

If you want, I can:
- Tailor the README with the exact project name(s) and project paths (please tell me where the main .csproj(s) live),
- Add badges (build / test / coverage) if you have CI configured,
- Create a LICENSE file and a basic CONTRIBUTING.md.
