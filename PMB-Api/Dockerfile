FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

WORKDIR /source
COPY *.sln .
COPY src/ ./src/
COPY libs/ ./libs/
COPY tests/ ./tests/

RUN dotnet restore

WORKDIR /source/

RUN dotnet publish --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY --from=build /app .
CMD ASPNETCORE_URLS=http://*:$PORT ./PMB.WebApi