FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /roomiesApp

COPY *.sln .

COPY src/Roomies2.DAL/*.csproj ./src/Roomies2.DAL/
COPY src/Roomies2.DAL.Tests/*.csproj ./src/Roomies2.DAL.Tests/
COPY src/Roomies2.DB/*.csproj ./src/Roomies2.DB/
COPY src/Roomies2.WebApp/*.csproj ./src/Roomies2.WebApp/

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
COPY --from=build /roomiesApp/src/out ./
ENTRYPOINT ["dotnet", "Roomies2.dll"]
