#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AspTestProject/AspTestProject.csproj", "AspTestProject/"]
COPY ["AspTestProject.BLL/AspTestProject.BLL.csproj", "AspTestProject.BLL/"]
COPY ["AspTestProject.Common/AspTestProject.Common.csproj", "AspTestProject.Common/"]
COPY ["AspTestProject.DAL/AspTestProject.DAL.csproj", "AspTestProject.DAL/"]
RUN dotnet restore "AspTestProject/AspTestProject.csproj"
COPY . .
WORKDIR "/src/AspTestProject"
RUN dotnet build "AspTestProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspTestProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspTestProject.dll"]