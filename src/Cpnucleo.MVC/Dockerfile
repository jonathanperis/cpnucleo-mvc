FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

LABEL maintainer "Jonathan Peris"

USER app
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["src/Cpnucleo.MVC/Cpnucleo.MVC.csproj", "src/Cpnucleo.MVC/"]

RUN dotnet restore "src/Cpnucleo.MVC/Cpnucleo.MVC.csproj"

COPY . .

WORKDIR "/src/src/Cpnucleo.MVC"

RUN dotnet build "Cpnucleo.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cpnucleo.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cpnucleo.MVC.dll"]