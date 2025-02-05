## Stock Tracker API

### Introduction

The Stock Tracker API is a robust ASP.NET Core Web API designed to facilitate the creation and management of stock portfolios. This application allows users to create stocks, comment on them, and manage their portfolios through a set of well-defined CRUD (Create, Read, Update, Delete) operations.

Built using the Repository Pattern, this API promotes a clean separation of concerns, ensuring that each component of the application has a distinct responsibility. The use of Entity Framework Core (EF Core) enables seamless database interactions, while SQLite serves as the lightweight database solution for development and testing.

### Key Features

- **User Authentication**: Implemented using ASP.NET Core Identity with JWT (JSON Web Tokens) for secure user authentication and authorization.
- **Data Transfer Objects (DTOs)**: Utilizes DTOs to ensure data integrity and security, preventing direct access to the database.
- **Dependency Injection**: Employs dependency injection to promote loose coupling and enhance testability of components.
- **Clean Architecture**: Follows the principles of clean architecture, promoting modularity and maintainability.
- **SOLID Principles**: Adheres to SOLID principles to ensure code quality and maintainability. 

#### Examples 
- Single Responsibility Principle (SRP): Each class (e.g., controllers, repositories, DTOs) seems to have a single responsibility. For instance, the PortfolioController manages portfolio-related actions, while the StockRepository handles data access for stocks.
- Liskov Substitution Principle (LSP): If interfaces are used correctly (e.g., IStockRepository, ICommentRepository), derived classes can be substituted without affecting the correctness of the program.
- Interface Segregation Principle (ISP): The use of specific interfaces for repositories suggests adherence to ISP, as clients are not forced to depend on methods they do not use.

This project serves as a practical guide for developers looking to understand and implement best practices in ASP.NET Core Web API development, particularly in the context of building applications that require user interaction and data management.