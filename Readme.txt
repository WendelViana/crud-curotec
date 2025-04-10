# 🛠️ CRUD API with ASP.NET Core, EF Core & FluentValidation

This project is a RESTful API developed with ASP.NET Core with CRUD operations for Curotec assessment.

## 📚 Features

- CRUD operations for `Product` resource
- Validation using FluentValidation 
- Error handling using custom middleware
- Repository and Service pattern with clear separation of concerns
- Batch creation with async processing and performance optimization (using Parallel)
- Clean architecture with Domain, Application, and Infrastructure layers
- EF Core integration with SQL Server

## Technologies Used

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- FluentValidation
- AutoMapper
- Dependency Injection
- LINQ & TPL
- Swagger / OpenAPI

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)


#### API Endpoints Overview

Method			Endpoint				Description
GET				/api/products			Get all products
GET				/api/products/{id}		Get product by ID
POST			/api/products			Create a new product
POST			/api/products/batch		Create multiple products (async + optimized)
PUT				/api/products			Update existing product
DELETE			/api/products/{id}		Delete a product


Author
Wendel Viana
.NET Developer | 12+ years of experience
Passionate about software and tech universe.
https://www.linkedin.com/in/wendel-viana-wvp/?locale=en_US