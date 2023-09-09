# .Net6  REST API | MongoDB | VSCode | .Net-CLI

## Set Up
### Content
the final project folder structure will be
```
.
├── README.md
├── api
│   ├── Controllers
│   │   └── StudentController.cs
│   ├── Program.cs
│   ├── Properties
│   │   └── launchSettings.json
│   ├── Services
│   │   ├── IStudentService.cs
│   │   └── TEST.cs
│   ├── WeatherForecast.cs
│   ├── api.csproj
│   ├── appsettings.Development.json
│   └── appsettings.json
├── console-ui
│   ├── Program.cs
│   └── console-ui.csproj
├── mongodb-demo.sln
└── omnisharp.json
```
### Create Solution
### Create Projects
### Binding Project
### Install Package in API Projects
```
cd api
dotnet add package MongoDB.Driver
```

## Adding CRUD in `API Project`
### Create a `Models` Folder 
#### Create a `Student` Class
#### Mongo Attributes 
    - [BsonIgnoreExtraElements] : allow data will ignore if the data has more value
    - [BsonId]
    - [BsonRepresentation(BsonType.ObjectId)]
    - [BsonElement("firstname")]
### Create All MongoDB Connection Infos in `appsettings.json`
```json
{
  "MongoDatabaseSettings": {
    "DatabaseName": "SideProject",
    "ConnectionString": "mongodb+srv://<username>:<passsword>@blacmarc.xl78bo3.mongodb.net/?retryWrites=true&w=majority",
    "StudentCollectionName": "studentcourses"
  }
}
```
#### UrlEncode
if using mongoDb cloud, you must notice the url must be encode(ex: your password). So you can use this web to encode your password`https://www.urlencoder.org/`
#### Create a `DataBaseSetting Interface` in Models folder
in that interface you will add variables  as same as `appsettings.json`
```c#
// IStudentStoreDatabaseSettings.cs
namespace api.Models;

public interface IStudentStoreDatabaseSettings
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
    string StudentCollectionName { get; set; }
}

```
#### Create a `DataBaseSetting Class` in Models folder

### Create CRUD
<br/>

## Adding JWT Authentications



## Source
- MongoDB
    https://www.youtube.com/watch?v=iWTdJ1IYGtg
- JWT
    https://www.youtube.com/watch?v=v7q3pEK1EA0
