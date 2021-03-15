FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY . .
RUN dotnet restore
ENTRYPOINT ["dotnet", "test"]