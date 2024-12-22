FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-stage
WORKDIR /app

COPY . ./

RUN dotnet restore EclipseWorks.TaskManagement.sln

RUN dotnet publish src/EclipseWorks.TaskManagement.WebApi/EclipseWorks.TaskManagement.WebApi.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime-stage
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080 

EXPOSE 8080

COPY --from=build-stage /publish .

ENTRYPOINT ["dotnet", "EclipseWorks.TaskManagement.WebApi.dll"]