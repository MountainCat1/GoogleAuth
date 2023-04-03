FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SimpleAccount/SimpleAccount.csproj", "SimpleAccount/"]
RUN dotnet restore "SimpleAccount/SimpleAccount.csproj"
COPY . .
WORKDIR "/src/SimpleAccount"
RUN dotnet build "SimpleAccount.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleAccount.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleAccount.dll"]
