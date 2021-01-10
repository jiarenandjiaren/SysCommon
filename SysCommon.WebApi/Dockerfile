#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SysCommon.WebApi/SysCommon.WebApi.csproj", "SysCommon.WebApi/"]
COPY ["src/Essensoft.AspNetCore.WeChatPay/Essensoft.AspNetCore.WeChatPay.csproj", "src/Essensoft.AspNetCore.WeChatPay/"]
COPY ["src/Essensoft.AspNetCore.Security/Essensoft.AspNetCore.Security.csproj", "src/Essensoft.AspNetCore.Security/"]
COPY ["SysCommon.Service/SysCommon.Service.csproj", "SysCommon.Service/"]
COPY ["src/Essensoft.AspNetCore.Alipay/Essensoft.AspNetCore.Alipay.csproj", "src/Essensoft.AspNetCore.Alipay/"]
COPY ["SysCommon.Repository/SysCommon.Repository.csproj", "SysCommon.Repository/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "SysCommon.WebApi/SysCommon.WebApi.csproj"
COPY . .
WORKDIR "/src/SysCommon.WebApi"
RUN dotnet build "SysCommon.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SysCommon.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SysCommon.WebApi.dll"]