FROM microsoft/dotnet:latest

COPY ImageProcessorCore /app/ImageProcessorCore
COPY GetImages /app/GetImages
COPY RayTraceCore /app/RayTraceCore
COPY ITTalk /app/img-generator

WORKDIR /app
RUN ["dotnet", "restore"]

WORKDIR /app/img-generator

RUN ["dotnet", "build"]
ENTRYPOINT ["dotnet", "run"]
CMD ["1"]