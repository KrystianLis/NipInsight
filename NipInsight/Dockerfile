FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings__DefaultConnection=Server=db;Database=master;User=sa;Password=P@ssword1;TrustServerCertificate=True;MultipleActiveResultSets=true;

ENTRYPOINT ["dotnet", "NipInsight.dll"]
