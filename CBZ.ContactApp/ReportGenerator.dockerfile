# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY CBZ.ContactApp/*.csproj ./CBZ.ContactApp/
COPY CBZ.ContactApp.Data/*.csproj ./CBZ.ContactApp.Data/
COPY CBZ.ContactApp.ReportGenerator/*.csproj ./CBZ.ContactApp.ReportGenerator/
COPY CBZ.ContactApp.Test/*.csproj ./CBZ.ContactApp.Test/
#
RUN dotnet restore
#
# copy everything else 
COPY CBZ.ContactApp/. ./CBZ.ContactApp/
COPY CBZ.ContactApp.Data/. ./CBZ.ContactApp.Data/
COPY CBZ.ContactApp.ReportGenerator/. ./CBZ.ContactApp.ReportGenerator/
COPY CBZ.ContactApp.Test/. ./CBZ.ContactApp.Test/
#
# make unit tests
RUN dotnet test
#
# build contactapp
#
WORKDIR /app/CBZ.ContactApp.ReportGenerator
RUN dotnet publish -c Release -o out
#
# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
WORKDIR /app
#
COPY --from=build /app/CBZ.ContactApp.ReportGenerator/out ./
ENTRYPOINT ["dotnet", "CBZ.ContactApp.ReportGenerator.dll"]