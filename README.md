# contact-app
 
# Pre Requisities
* Install Latest version of the Docker to run project and docker-compose
* Install .Net 5.0 for contributing development and Unit Tests
* Postman for Example requests and some endpoint testing

# Build containers
```bash
cd ./contact-app/CBZ.ContactApp
docker build -f ./ContactApp.dockerfile -t cbz-contactapp .
docker build -f ./ReportGenerator.dockerfile -t cbz-reportgenerator .
```

# Docker compose up
