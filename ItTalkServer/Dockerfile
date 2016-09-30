FROM microsoft/dotnet:latest

RUN echo '<?xml version="1.0" encoding="utf-8"?> \
<configuration> \
<packageSources> \
<add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" /> \
<add key="vlogging" value="https://www.myget.org/F/aspnetvnext" /> \
</packageSources> \
</configuration>' > /root/.nuget/NuGet/NuGet.Config

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]
