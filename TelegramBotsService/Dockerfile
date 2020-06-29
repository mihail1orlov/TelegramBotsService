FROM mcr.microsoft.com/dotnet/core/sdk:latest 
COPY . /app 
WORKDIR /app
RUN ["dotnet", "restore"] 
RUN ["dotnet", "build"] 
WORKDIR /app/Host
ENTRYPOINT ["dotnet", "run"]