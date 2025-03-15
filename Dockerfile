FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8050

ENV ASPNETCORE_URLS=http://+:8050

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY ["TestExecution.Api/TestExecution.Api.csproj", "TestExecution.Api/"]
COPY ["TestExecution.Service/TestExecution.Service.csproj", "TestExecution.Service/"]
COPY ["TestExecution.Data/TestExecution.Data.csproj", "TestExecution.Data/"]
COPY ["TestExecution.Domain/TestExecution.Domain.csproj", "TestExecution.Domain/"]
RUN dotnet restore "TestExecution.Api/TestExecution.Api.csproj"
COPY . .
WORKDIR "TestExecution.Api"
RUN dotnet publish "TestExecution.Api.csproj" -c Release -o /app/publish

# Copy the published app to the base image and configure it to run
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TestExecution.Api.dll"]

