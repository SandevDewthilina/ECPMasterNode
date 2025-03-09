FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY artifacts/publish/ ./

ENV ASPNETCORE_URLS=http://0.0.0.0:6555
ENV ASPNETCORE_ENVIRONMENT=Development

RUN mkdir -p /root/Test_systems/Cherish_V3/publish

EXPOSE 6555
EXPOSE 3306

ENTRYPOINT ["dotnet", "ERP.Web.dll"]