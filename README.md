# Management API

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![CI/CD](https://github.com/lenz-moraga/management-api/actions/workflows/copilot-swe-agent/copilot/badge.svg)](https://github.com/lenz-moraga/management-api/actions)
[![Deploy Status](https://img.shields.io/badge/AWS-Coming%20Soon-orange)](https://github.com/lenz-moraga/management-api)

A modern, scalable RESTful API built with **.NET 8** following **Clean Architecture** principles. This project serves as a comprehensive management system designed for learning, portfolio demonstration, and real-world application development.

## 🎯 Project Purpose

The Management API is a production-ready backend solution that demonstrates best practices in modern web API development. It showcases:

- **Clean Architecture** implementation with clear separation of concerns
- Enterprise-grade patterns and practices
- Multi-environment configuration management
- Cloud-native deployment strategies

### Use Cases

- **Learning Resource**: Study Clean Architecture principles in a real-world .NET application
- **Portfolio Project**: Showcase modern backend development skills
- **Startup MVP**: Foundation for building management and CRUD-based applications
- **Microservices Template**: Starting point for distributed system architectures

### Target Audience

- Backend developers learning Clean Architecture
- Teams building RESTful APIs with .NET
- Students and professionals building portfolio projects
- Organizations seeking a solid API foundation

## 🛠️ Technology Stack

| Category | Technology |
|----------|-----------|
| **Framework** | .NET 8 |
| **Architecture** | Clean Architecture |
| **Database** | PostgreSQL |
| **ORM** | Entity Framework Core |
| **Containerization** | Docker & Docker Compose |
| **API Documentation** | Swagger/OpenAPI |
| **Cloud Platform** | AWS (planned) |
| **Database Service** | Supabase (development) |

### Why Clean Architecture?

This project follows **Clean Architecture** principles to ensure:

- **Independence**: Business logic is decoupled from frameworks, UI, and databases
- **Testability**: Core business rules can be tested without external dependencies
- **Maintainability**: Clear boundaries make the codebase easier to understand and modify
- **Flexibility**: Easy to swap out infrastructure components without affecting business logic

## ✨ Features

- **CRUD Operations**: Complete Create, Read, Update, Delete functionality
- **Multi-Environment Support**: Development, Staging, and Production configurations
- **Database Migrations**: Automated schema management with EF Core migrations
- **API Documentation**: Interactive Swagger UI for testing and exploration
- **Dockerized Development**: Consistent development environment across teams
- **RESTful Design**: Standard HTTP methods and status codes
- **Error Handling**: Comprehensive exception handling and logging
- **Validation**: Input validation with clear error messages

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [PostgreSQL](https://www.postgresql.org/download/) (or use Supabase)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/lenz-moraga/management-api.git
   cd management-api
   ```

2. **Configure environment variables**
   ```bash
   cp appsettings.Example.json appsettings.Development.json
   # Edit appsettings.Development.json with your database connection string
   ```

3. **Run with Docker Compose**
   ```bash
   docker-compose up -d
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:5001` (or the configured port).

## 🗄️ Database Setup

### Using Migrations

This project uses **Entity Framework Core Migrations** for database schema management:

```bash
# Create a new migration
dotnet ef migrations add MigrationName

# Update database to latest migration
dotnet ef database update

# Rollback to a specific migration
dotnet ef database update PreviousMigrationName
```

### Using Supabase (Recommended for Development)

[Supabase](https://supabase.com/) provides a free PostgreSQL database perfect for development:

1. Create a free account at [supabase.com](https://supabase.com/)
2. Create a new project
3. Copy the connection string from Project Settings → Database
4. Update your `appsettings.Development.json` with the connection string

**⚠️ Free Tier Limitation**: Supabase free plan includes 500MB database space and 2 projects. Ideal for development and small projects.

### Local PostgreSQL

Alternatively, use Docker to run PostgreSQL locally:

```bash
docker run --name management-db \
  -e POSTGRES_PASSWORD=yourpassword \
  -e POSTGRES_DB=managementdb \
  -p 5432:5432 \
  -d postgres:16
```

## 📚 API Documentation

### Swagger UI

Once the application is running, access the interactive API documentation at:

```
https://localhost:5001/swagger
```

Swagger UI provides:
- Complete API endpoint listing
- Request/response schemas
- Interactive testing interface
- Authentication testing

### Example API Calls

**Get all items**
```bash
curl -X GET "https://localhost:5001/api/items" -H "accept: application/json"
```

**Create a new item**
```bash
curl -X POST "https://localhost:5001/api/items" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Sample Item",
    "description": "This is a test item"
  }'
```

**Get item by ID**
```bash
curl -X GET "https://localhost:5001/api/items/1" -H "accept: application/json"
```

**Update an item**
```bash
curl -X PUT "https://localhost:5001/api/items/1" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Updated Item",
    "description": "Updated description"
  }'
```

**Delete an item**
```bash
curl -X DELETE "https://localhost:5001/api/items/1" -H "accept: application/json"
```

## 🗺️ Roadmap

### Current Version (v1.0)
- ✅ Clean Architecture foundation
- ✅ Basic CRUD operations
- ✅ PostgreSQL integration
- ✅ Docker support
- ✅ Swagger documentation

### Future Enhancements
- 🔄 Authentication & Authorization (JWT)
- 🔄 Advanced filtering and pagination
- 🔄 Caching layer (Redis)
- 🔄 Background job processing
- 🔄 Event-driven architecture
- 🔄 GraphQL API endpoint
- 🔄 Rate limiting
- 🔄 Comprehensive logging (Serilog)
- 🔄 Health checks and monitoring
- 🔄 CI/CD pipeline with automated tests

## 🌐 Deployment

### Live Demo

**🚧 Coming Soon**: The production deployment on AWS will be available soon.

**Demo URL**: _Will be updated once deployed_

### AWS Deployment (Planned)

The application will be deployed on AWS using:
- **ECS/Fargate**: Container orchestration
- **RDS PostgreSQL**: Managed database service
- **Application Load Balancer**: Traffic distribution
- **CloudWatch**: Logging and monitoring
- **Route 53**: DNS management

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Lenz Moraga**

- GitHub: [@lenz-moraga](https://github.com/lenz-moraga)

## 🙏 Acknowledgments

- Clean Architecture principles by Robert C. Martin
- .NET Community for excellent resources and support
- Supabase for providing free PostgreSQL hosting

---

**⭐ If you find this project useful, please consider giving it a star!**
