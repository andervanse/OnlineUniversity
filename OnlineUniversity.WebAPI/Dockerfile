#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.0 AS build
WORKDIR /src
COPY ["OnlineUniversity.WebAPI/OnlineUniversity.WebAPI.csproj", "OnlineUniversity.WebAPI/"]
RUN dotnet restore "OnlineUniversity.WebAPI/OnlineUniversity.WebAPI.csproj"
COPY . .
WORKDIR "/src/OnlineUniversity.WebAPI"
RUN dotnet build "OnlineUniversity.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineUniversity.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineUniversity.WebAPI.dll"]