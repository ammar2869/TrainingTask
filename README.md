# Trainee Management .NET API
 
## Tech Stack
- Framework : .NET 10 / ASP.NET Core
- Database : Entity Framework Core In Memory Database
- API Documentation : Swagger
- Language : C#
 
## API Endpoints
 
 
|Method|   Endpoint | Description |
|--------| --------| -------- |
|`GET`| `/api/health`  | Check API Status  |
|`GET`|`/api/Trainee` | Fetch all trainees from db|
|`GET`|`/api/Trainee?={searchQuery}` | Fetch trainees based on searchQuery|
|`GET`|`/api/Trainee/{id}` | Fech a particular trainee by ID |
|`POST`|`/api/Trainee` | Post trainee  |
|`PUT`|`/api/Trainee/{id}` | Update trainee  |
|`DELETE`|`/api/Trainee/{id}` | Delete trainee  |

## Database Setup (MySQL)

1. Install MySql Server
```
sudo apt install mysql-server -y
```

2. Start MySql Server
```
sudo service mysql start
```
3. Run the server
```
mysql -u root -p
```

4. Update Connection String
Edit your appsettings.json
```
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=TraineeDB;user=root;password=yourpassword"
}
```

5. Install Required Packages
```
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

## How to Run
1. Clone the Github Repo
```bash
git clone https://github.com/ammar2869/TrainingTask.git
```

2. Checkout DotnetTask branch
```bash
git checkout "DotnetTask"
```

3. Navigate to the project
```bash
cd TraineeManagement
```

4. Run the Project
```bash
dotnet run
```

5. Open Swagger UI
```bash
http://localhost:5021/index.html
```

## Sample Request JSON

```
{
  "firstName": "Mohammed Ammar",
  "lastName": "Karimi",
  "email": "ammar@gmail.com",
  "techStack": "Python",
  "status": "Available"
}
```

## Sample JSON Response
Success Response for Create and Get All Trainee Details
```
{
  "id": 1,
  "firstName": "Mohammed Ammar",
  "lastName": "Karimi",
  "email": "ammar@gmail.com",
  "techStack": "Python",
  "status": "Available",
  "createdDate": "2026-06-08T10:51:30.5409469Z",
  "updatedDate": "2026-06-08T10:51:30.540947Z"
}
```

Success Response for PUT and GET Trainee by ID
```
{
  "firstName": "Mohammed Ammar",
  "lastName": "Karimi",
  "email": "ammar@gmail.com",
  "techStack": "Python",
  "status": "Available",
  "updatedDate": "2026-06-08T10:51:30.540947Z"
}
```
## Limitations
- Uses EF Core InMemory Database (data resets on restart)
- Limited Validation Rules
- API Security isn't been implemented

