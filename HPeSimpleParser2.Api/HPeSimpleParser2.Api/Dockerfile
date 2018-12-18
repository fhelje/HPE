FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["HPeSimpleParser2.Api/HPeSimpleParser2.Api.csproj", "HPeSimpleParser2.Api/"]
RUN dotnet restore "HPeSimpleParser2.Api/HPeSimpleParser2.Api.csproj"
COPY . .
WORKDIR "/src/HPeSimpleParser2.Api"
RUN dotnet build "HPeSimpleParser2.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HPeSimpleParser2.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HPeSimpleParser2.Api.dll"]