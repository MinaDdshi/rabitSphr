# Build

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:latest AS build

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /source

COPY . .

RUN dotnet restore "./RedditMockup.Web/RedditMockup.Web.csproj" --disable-parallel

RUN dotnet publish "./RedditMockup.Web/RedditMockup.Web.csproj" -o /app --no-restore

# Serve

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

COPY --from=build /app ./

ENTRYPOINT ["dotnet", "RedditMockup.Web.dll"]
