#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebEvaluacionCoreApi/WebEvaluacionCoreApi.csproj", "src/WebEvaluacionCoreApi/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Persistence/Persistence.csproj", "src/Persistence/"]
RUN dotnet restore "src/WebEvaluacionCoreApi/WebEvaluacionCoreApi.csproj"
COPY . .
WORKDIR "/src/src/WebEvaluacionCoreApi"
RUN dotnet build "WebEvaluacionCoreApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebEvaluacionCoreApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY ["src/WebEvaluacionCoreApi/libfacesdk.so","./"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebEvaluacionCoreApi.dll"]