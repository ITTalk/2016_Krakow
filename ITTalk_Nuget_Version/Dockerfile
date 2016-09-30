FROM microsoft/dotnet:latest

COPY ITTalk /app/img-generator
COPY Vendors /app/Vendors


WORKDIR /app/img-generator
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
ENTRYPOINT ["dotnet", "run"]
CMD ["1"]