# README #

# Borrowing System API

A backend RESTful API for managing a borrowing system, built with **.NET 8**, **PostgreSQL**, and **Docker**, following 
clean architecture principles and supporting multi-environment configuration.

---

## How do I get set up?

### 1. Clone the repository

```bash
git clone git@github.com:lenz-moraga/management-api.git
cd management-api
```

### 2. Create the .env.local and the appsettings.Development.json (if debugging in visual studio)

ConnectionStrings__DefaultConnection=User Id=...;Password=...;Host=...;Port=5432;Database=...
AppSettings__Token=your_very_secure_token

### 3. Development Setup

To enable live reload and fast iteration:

```bash
docker-compose -f docker-compose.override.yml up --build
```

#### This will:

- Mount the source code into the container
- Use dotnet watch to restart on file changes
- Use the Debug build configuration

You can also run from Visual Studio for debugging the project:

- Make sure the launch profile uses ASPNETCORE_ENVIRONMENT=Development
- Set the appSettings.Development.json settings with the env vars
- Press F5 to start debugging

### 4. API Documentation
Swagger is only enabled in development mode.

Navigate to: http://localhost:8080/swagger/index.html

### 5. Migrations

```bash
dotnet ef migrations add "Migration message" --project BorrowingSystem.WebApi
---
dotnet ef database update --project BorrowingSystem.WebApi
```

### Contribution guidelines ###

To contribute to the project, use the `.github/PULL_REQUEST_TEMPLATE.md` file structure, this way we will have more 
and detailed information about the changes.

* Writing tests
* Code review
