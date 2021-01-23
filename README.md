# contact-app
 
# Pre Requisities
* Install Latest version of the Docker to run project and docker-compose
* Install .Net 5.0 for contributing development and Unit Tests
* Postman for Example requests and some endpoint testing

# Build projects containers with docker files
```bash
cd ./contact-app/CBZ.ContactApp
docker build -f ./ContactApp.dockerfile -t cbz-contactapp:1 .
docker build -f ./ReportGenerator.dockerfile -t cbz-reportgenerator:1 .
```
# Docker compose up 
```bash
cd ./contact-app
docker-compose up -d
```
# PgAdmin4
* url: http://localhost:8080
* eposta: admin@contactapp.com
* pass: secret

# PgAdmin4 -pqsql connection 
    * leftPane under "Browser", "Servers" right click on "Servers"=>Create=>Create Server
    * Name:postgres
    * Hostname : pqsql
    * Port :5432
    * Username:admin
    * pass:secret

# RappitMq-Management
* url: http://localhost:15672
* eposta: admin
* pass: secret

# Postman example collections
* https://www.getpostman.com/collections/4d17be68d86a58e18c0d


# How to run
```bash
cd ./contact-app/CBZ.ContactApp
dotnet run --project CBZ.ContactApp
dotnet run --project CBZ.ContactApp.ReportGenerator
```

