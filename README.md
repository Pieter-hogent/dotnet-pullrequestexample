# Rise - [GROUPNAME]

## Team Members
- [MEMBER1_NAME] - [MEMBER1_EMAIL] - [MEMBER1_GITHUB_USERNAME]

## Technologies Used
- [FILLIN] 
- [FILLIN]
- [FILLIN]

## Installation Instructions
1. Clone the repository
2. ... [FILLIN]


## Migration of the database
There is a default database that is used for this project. The intial migrations was already created using the following command:
```
dotnet ef migrations add Initial --startup-project Rise.Server --project Rise.Persistence
```
> Note: The migrations are stored in the `Rise.Persistence/Migrations` folder.
To create the database, run the following command:
```
dotnet ef database update --startup-project Rise.Server --project Rise.Persistence
```
> Make sure your connection string is correct in the `Rise/Server/appsettings.json` file.

For future migrations, you can create a new migration using the following command:
```
dotnet ef migrations add [MIGRATION_NAME] --startup-project Rise.Server --project Rise.Persistence
```
And then update the database using the following command:
```
dotnet ef database update --startup-project Rise.Server --project Rise.Persistence
```
