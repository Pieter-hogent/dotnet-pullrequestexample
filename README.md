# Rise - [GROUPNAME]

## Team Members
- [MEMBER1_NAME] - [MEMBER1_EMAIL] - [MEMBER1_GITHUB_USERNAME]

## Technologies & Packages Used
- [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) - Frontend
- [ASP.NET 8](https://dotnet.microsoft.com/en-us/apps/aspnet) - Backend
- [Entity Framework 8](https://learn.microsoft.com/en-us/ef/) - Database Access
- [EntityFrameworkCore Triggered](https://github.com/koenbeuk/EntityFrameworkCore.Triggered) - Database Triggers
- [User Secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets) - Securely store secrets in DEV.
- [GuardClauses](https://github.com/ardalis/GuardClauses) - Validation Helper
- [bUnit](https://bunit.dev) - Blazor Component Testing
- [xUnit](https://xunit.net) - (Unit) Testing
- [nSubstitute](https://nsubstitute.github.io) - Mocking for testing
- [Shouldly](https://docs.shouldly.org) - Helper for testing

## Installation Instructions
1. Clone the repository
2. Open the `Rise.sln` file in Visual Studio or Visual Studio Code
3. Run the project using the `Rise.Server` project as the startup project
4. The project should open in your default browser on port 5001.
5. Initially the database will not exist, so you will need to run the migrations to create the database.

## Creation of the database
To create the database, run the following command in the main folder `Rise`
```
dotnet ef database update --startup-project Rise.Server --project Rise.Persistence
```
> Make sure your connection string is correct in the `Rise/Server/appsettings.json` file.

## Migrations
Adapting the database schema can be done using migrations. To create a new migration, run the following command:
```
dotnet ef migrations add [MIGRATION_NAME] --startup-project Rise.Server --project Rise.Persistence
```
And then update the database using the following command:
```
dotnet ef database update --startup-project Rise.Server --project Rise.Persistence
```

## Authentication
In the `main` branch, there is no authentication present. You can switch to the [auth](https://github.com/HOGENT-RISE/dotnet-template/tree/auth) branch and checkout the commits from "Authenticating Client With Auth0" forward to the last commit to see how to authenticate your users using [Auth0](https://auth0.com).
There are a few articles which explain what is done. It's best to start from the first one and sequentially go through them by starting with [Blazor Authentication with Auth0](https://benjaminvertonghen.medium.com/role-based-acces-control-with-blazor-and-auth0-i-ffd9656e6f01)

## Solution Structure Overview

The template is designed as a boilerplate or template for .NET solutions, following best practices for structuring projects, separation of concerns, and maintainability. Here's a breakdown of the solution structure and its workings, explained:

When you open the solution, you’ll notice it’s organized into multiple projects, which is a common approach in large, enterprise-level applications. Each project within the solution has a specific responsibility. This approach is based on the **Clean Architecture** or **Domain-Driven Design (DDD)** principles. The goal is to keep different aspects of the application separated and independent, making it easier to scale, maintain, and test.

Here are the main projects in the solution:

1. **Domain**
2. **Services**
3. **Persistence**
4. **Server**
5. **Client**
6. **Shared**

Let’s look at each of these in more detail:

------

### 1. **Domain Project**

**Folder**: `Domain`

**Purpose**: The **Domain** project holds the core logic of the application. It defines the business rules, which are independent of the UI, database, or any external technology. The principle here is to keep the domain logic isolated, making sure it’s not affected by external frameworks or infrastructure.

**Typical Contents**:

- **Entities**: Classes that represent the core objects of the application, such as `Order`, `Customer`, or `Product`.

- **Value Objects**: Immutable objects that represent a concept (like `Money` or `Address`).

  > Currently not provided in the template, but you can read more here: [Domain Driven Design - Best Practises](https://hogent-web.github.io/csharp/chapters/03/slides/index.html#75)

**Why this separation?**: Keeping the domain logic separate ensures that the business rules remain consistent even if the application’s presentation or infrastructure changes. This allows for flexibility and ensures that changes to other parts of the system don't break the business logic.

------

### 2. **Services Project**

**Folder**: `Services`

**Purpose**: The **Services** project is responsible for the application-specific logic, such as orchestrating use cases, handling commands, and queries, and processing workflows. It acts as an intermediary between the **Domain** and the **Infrastructure** or **API** layers.

**Typical Contents**:

- **Interfaces for Services**: Service contracts that will be implemented in the infrastructure / API / ... layer.
- **Use Cases**: These classes are responsible for specific actions in the system, like creating an order or processing a payment.

**Why this separation?**: This project enforces the **Separation of Concerns (SoC)**. By keeping application logic here, you can independently evolve your business logic from your API or database logic. It also makes testing easier, as this layer can be unit tested without worrying about external dependencies. 

> Note that we can swap out the API for something else, for example a console application and the business rules will still apply.
>
> We do not recommend abstracting your database as we see it as a migration to another database provider, not an abstraction. Read more about it [Should you Abstract the Database ](https://enterprisecraftsmanship.com/posts/should-you-abstract-database/).

------

### 3. **Persistence Project**

**Folder**: `Persistence`

**Purpose**: The **Persistence** project deals with Database mappings and database migrations, that's it.

**Typical Contents**:

- **Configurations**: Entity configurations, for example how a product is mapped to a table in SQL, using Entity Framework Core.
- **Data Migrations**: Scripts or classes for evolving the database schema over time.
- **Triggers**: Stuff that needs to happen when something is saved or retrieved from the database. It's rather optional but these triggers are database agnostic (they will work for any provider e.g. MariaDb, Microsoft SQL Server,... )

**Why this separation?**: So it's easier to find the configurations and keep them out of the **Domain** logic, Domain classes should **not** know how they're stored.

------

### 4. **Server Project**

**Folder**: `Server`

**Purpose**: The **API** project is the entry point for the application, where the HTTP endpoints are defined. It handles requests from clients (via RESTful HTTP requests) and returns responses. It uses **controllers** to expose application functionality to the outside world.

**Typical Contents**:

- **Controllers**: These handle HTTP requests and responses. They receive requests, pass them to the appropriate application service, and return the result.
- **Middleware**: Custom or third-party components that handle cross-cutting concerns such as logging, authentication, or error handling.
- **Dependency Injection Configuration**: The **Server** project contains the setup for the dependency injection container, where the various services and other dependencies are registered.
- **Serving the Blazor Client** : If no endspoints are found, the **Server** returns the Blazor WebAssembly (WASM) **Client**, it's rather optional but it makes hosting a lot easier.

**Why this separation?**: The **API** layer provides a clean separation between the user interface (UI) and the business logic. This project acts as the boundary between your back-end system and the outside world, and it enforces that external clients (e.g., mobile apps or front-end websites) communicate in a consistent and defined way.

------

### 5. **Client Project**

**Folder**: `Client` 

A Blazor Web Assembly Standalone client, just like React, Vue, Svelte, Angular,... but written in C#.

---

### 6. **Shared Project**

The **Shared** project is the glue between the **Client** and the **Server**. It decouples the Domain from the the Client, therefore we can still adjust the database , Services and Domain layer without breaking any clients. If we don't remove properties from the Data Transfer Objects (**DTO**)

- **Service Interfaces**: The contract between the **Client** and **API**.
- **Data Transfer Objects**: Simple classes without any domain logic. They're used to transfer data from the **API** to the **Client**.

------

### 7. **Testing Projects**

**Folder**: `Client.Tests` and `Domain.Tests`

While not always included in the base template, most well-architected solutions should have dedicated testing projects, typically organized into **Unit Tests**, **Integration Tests**, and possibly **End-to-End Tests**.

- **Unit Tests**: Test individual components (usually found in the `Domain` or `Client` layers) in isolation from dependencies.

- **Integration Tests**: Ensure different parts of the system work together correctly (e.g., API and database).

  > We did not provide any integration tests, there are for you team to figure out. 

By separating the tests into their own projects, you ensure that they remain maintainable, modular, and focused on the specific functionality being tested.

---

### 8. **Cross-Cutting Concerns**

In some solutions, you may see additional projects or services to handle **cross-cutting concerns** like **logging**, **caching**, **authorization**, or **exception handling**. These concerns can be plugged into multiple layers of the solution but are typically handled in the **Persistence** / **Infrastructure** and **Server** projects.

------

### Key Concepts Explained

1. **Separation of Concerns (SoC)**: Each project in the solution has a single, well-defined responsibility. By separating concerns, changes in one part of the system (e.g., switching databases) do not ripple through the entire codebase.
2. **Dependency Injection (DI)**: This design pattern is used to inject dependencies into classes. The **API** project often configures DI, so classes get the services or repositories they need without creating them directly. This promotes loose coupling and makes the code easier to test.
3. **Domain-Driven Design (DDD)**: The structure of the **Domain** project follows DDD principles, where the business rules and logic are core to the application and should be isolated from infrastructure concerns. This keeps your business logic intact even as external technologies evolve.

------

### Conclusion

The `dotnet-template` solution is structured to encourage scalability, maintainability, and testability. Each project serves a distinct purpose:

- **Domain** defines core business logic.
- **Services** manages use cases and orchestrates the flow of information.
- **Persistence** handles the interaction with external systems and data storage.
- **API** exposes the functionality to the outside world via HTTP.
- **Client** a User Interface that could be swapped if need be.