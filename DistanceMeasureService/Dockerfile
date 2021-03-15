FROM  mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
#COPY DistanceService.sln ./
COPY DistanceService.Domain/*.csproj ./DistanceService.Domain/
COPY DistanceService.Business/*.csproj ./DistanceService.Business/
COPY DistanceService.Rest/*.csproj ./DistanceService.Rest/

COPY . .
WORKDIR /src/DistanceService.Domain
RUN dotnet restore
RUN dotnet build -c Release -o /app

WORKDIR /src/DistanceService.Business
RUN dotnet restore
RUN dotnet build -c Release -o /app

WORKDIR /src/DistanceService.Rest
RUN dotnet restore
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DistanceService.Rest.dll"]