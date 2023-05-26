# Document storage system
This application is a system for storing documents in s3 storage. 
Users of the system can: 
- Upload files to the system
- Download files 
- Mark files for automatic deletion after the first download
All files are stored in the cloud, which ensures their safety through distributed storage.

## Technologies and libraries used: 
- ASP Net 7 - API
- EF 7 - ORM
- MediatR - Messaging
- SignalR - Real-time interaction
- Aws SDK - S3 SDK
- YandexObjectStorage - Cloud storage used as s3 storage
- Docker, Docker-compose - Containerization
- AutoMapper - Mapping

## Usage
After installing the application, open the Swagger file to view the definition of all endpoints.

## Getting started

### Docker
To pull a docker image, run the following command:
```
docker pull v4dikos/storagesystemapi:latest
```

### Docker-compose
In order to run the application, run the following command (must be in the same directory as the docker-compose.yml file):
```docker
docker-compose up -d
```
You may have to run the following commands to configure the certificate:
```
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p 65798732
dotnet dev-certs https --trust
```

### Git
To install the application, clone the git repository.
After cloning the repository, you will need to configure the appsetting.json file.

### The structure of appsettings.json:
```json
{
  "ConnectionStrings": {
      "DbConnection": "your_npgsql_connection_string"
  },
  "Jwt": {
    "Issuer": "issuer",
    "Audience": "audience",
    "SecretKey": "your_secret_key"
  },
  "EmailOptions": {
    "ConfirmUrl": "email_confirm_url",
    "SenderName": "sender_name",
    "SenderEmail": "sender_email",
    "SmtpHost": "smtp_host",
    "SmtpPort": "smtp_port",
    "SmtpUsername": "user_login_for_the_smtp_server",
    "SmtpServicePassword": "user_password"
  },
  "s3StorageOptions": {
    "ServiceUrl": "provider_url_of_s3_storage",
    "BucketName": "your_bucket_name",
    "ReceiptLink": "your_link_for_documents_return"
  },
  "Aws": {
    "ServiceURL": "https://s3.yandexcloud.net",
    "aws_access_key_id": "your_access_key_id",
    "aws_secret_access_key": "your_secret_access_key",
    "region": "ru-central1"
  },
  "AllowedHosts": "*"
}
```
- The **ConnectionStrings** section defines the database connection string.
- The **jwt** section defines the settings for the jwt-token.
- The **EmailOptions** section defines the settings for sending emails.
- The **s3StorageOptions** and **Aws** sections define the settings for s3 storage.
