#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM pre-build-params_registry/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM pre-build-params_registry/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY IdentityServer IdentityServer
COPY Domain Domain
COPY Infrastructure Infrastructure
WORKDIR "/src/IdentityServer"
RUN sed -i "s,<ReferenceOutputAssembly>False</ReferenceOutputAssembly>,<ReferenceOutputAssembly>True</ReferenceOutputAssembly>,g" ./IdentityServer.csproj
RUN dotnet nuget add source --username pre-build-params_gitlab-source-username --password pre-build-params_gitlab-source-token --store-password-in-clear-text --name github "pre-build-params_gitlab-source-url"
RUN dotnet restore "IdentityServer.csproj"
RUN dotnet build "IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /
COPY --from=publish /app/publish app
COPY --from=publish /app/build Infrastructure/bin/Debug/net6.0/
WORKDIR /app
ENTRYPOINT ["dotnet", "IdentityServer.dll"]