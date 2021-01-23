pipeline {
  environment {
    contact-app-image-name = "cbz/contact-app"
    report-gen-image-name = "cbz/report-gen"
    registryCredential = 'codeboz-docker-hub-token'
    contact-app-image = ''
    report-gen-image = ''
  }
  agent any
  stages {
    stage('Cloning Git') {
      steps {
        git([url: 'https://github.com/codeboz/contact-app.git', branch: 'master', credentialsId: 'codeboz-github-read-token'])

      }
    }
    stage('Building images') {
      steps{
        script {
          contact-app-image = docker.build(contact-app-image-name,"-f CBZ.ContactApp/ContactApp.dockerfile  CBZ.ContactApp")
        }
        script {
          report-gen-image = docker.build(report-gen-image-name,"-f CBZ.ContactApp/ReportGenerator.dockerfile  CBZ.ContactApp")
        }
      }
    }
    stage('Deploy images') {
      steps{
        script {
          docker.withRegistry( '', registryCredential ) {
            contact-app-image.push("$BUILD_NUMBER")
            contact-app-image.push('latest')
          }
        }
        script {
          docker.withRegistry( '', registryCredential ) {
            report-gen-image.push("$BUILD_NUMBER")
            report-gen-image.push('latest')
          }
        }
      }
    }
    stage('Remove Unused docker image') {
      steps{
        sh "docker rmi $contact-app-image:$BUILD_NUMBER"
        sh "docker rmi $contact-app-image:latest"
        sh "docker rmi $report-gen-image:$BUILD_NUMBER"
        sh "docker rmi $report-gen-image:latest"
      }
    }
  }
}