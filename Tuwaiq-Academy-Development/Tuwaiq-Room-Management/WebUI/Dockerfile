FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebUI/WebUI.csproj", "WebUI/"]
COPY ["SDK/SDK.csproj", "SDK/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet nuget add source https://nuget.tuwaiqdev.com/v3/index.json -n BaGet

RUN dotnet restore "WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


RUN apt-get update && apt-get install -y openssl

RUN openssl pkcs12 -in /app/tuwaiq.pfx -passin pass:P@ssw0rd@Tuwaiq@007 -nokeys -out /app/tuwaiq.crt && \
    openssl pkcs12 -in /app/tuwaiq.pfx -passin pass:P@ssw0rd@Tuwaiq@007 -nodes -nocerts -out /app/tuwaiq.key 

RUN cp /app/tuwaiq.crt /usr/local/share/ca-certificates/ && update-ca-certificates

RUN openssl pkcs12 -in /app/tuwaiqdev.pfx -passin pass:P@ssw0rd@Tuwaiq@007 -nokeys -out /app/tuwaiqdev.crt && \
    openssl pkcs12 -in /app/tuwaiqdev.pfx -passin pass:P@ssw0rd@Tuwaiq@007 -nodes -nocerts -out /app/tuwaiqdev.key 

RUN cp /app/tuwaiqdev.crt /usr/local/share/ca-certificates/ && update-ca-certificates

ENV ASPNETCORE_URLS=https://+:443;http://+:80

ENTRYPOINT ["dotnet", "WebUI.dll"]
