# My Cookbook

## About the project

**My Cookbook** is an API developed in **.NET** for managing culinary recipes.

The application allows users to register, log in, and manage their own recipes in a simple and organized way. Each recipe can include a title, ingredients, preparation steps, cooking time, difficulty level, and an illustrative image.

In addition to the recipe CRUD, the project also covers common features found in real-world applications, such as JWT authentication, Refresh Tokens, Google Login, image uploads, AI integration, messaging, automated tests, CI/CD pipelines, and code coverage analysis.

This project was built with a focus on backend development best practices, code organization, and creating an API close to what is used in the job market.

---

## Features

- **User registration**  
  Allows users to create an account using their name, email, and password.

- **Secure authentication**  
  Implementation of JWT authentication and Refresh Tokens.

- **Google Login**  
  Integration with Google account authentication.

- **Recipe management**  
  Creation, editing, deletion, listing, and filtering of recipes.

- **Image upload**  
  Allows adding an illustrative image for each recipe.

- **AI Integration**  
  Generation of complete recipes with images, powered by artificial intelligence.

- **Messaging**  
  Use of Azure Service Bus for asynchronous processing, such as account deletion.

- **Multiple database support**  
  Compatible with MySQL and SQL Server.

- **Automated tests**  
  Unit tests and integration tests to ensure application quality.

- **CI/CD**  
  Pipeline configured in Azure DevOps featuring build, tests, and coverage analysis.

- **Code coverage analysis**  
  Visualization and interpretation of test coverage to identify parts of the code that need more validation.

---

## Built with

![badge-c#]
![badge-dot-net]
![badge-windows]
![badge-visual-studio]
![badge-mysql]
![badge-sqlserver]
![badge-swagger]
![badge-docker]
![badge-azure-devops]
![badge-azure]
![badge-yaml]
![badge-gmail]
![badge-openai]

---

## Architecture

The project follows an organization based on **Domain-Driven Design (DDD)**, with a separation of concerns between the application layers and shared projects.

The main structure of the solution is organized as follows:

```txt
src/
 ├── Backend/
 │   ├── Api/
 │   ├── Application/
 │   ├── Domain/
 │   └── Infrastructure/
 │
 └── Shared/
     ├── Communication/
     └── Exception/

tests/
 ├── UseCases.Tests/
 ├── Validators.Tests/
 └── WebApi.Tests/
```

### Main responsibilities

#### Backend

Contains the main projects of the API and concentrates the application rules, workflows, and integrations.

- **API**
  Responsible for exposing endpoints, configuring middlewares, authentication, Swagger documentation, and application startup.

- **Application**
  Contains the application use cases (Business rules) with the necessary validations and transformations to execute the system workflows.

- **Domain**
  Contains entities and core contracts.

- **Infrastructure**
  Responsible for external implementations, such as database access, authentication services, email sending, integrations, and persistence.

#### Shared

Contains shared projects that can be used by different parts of the solution.

- **Communication**
  Contains the objects used in API communication, such as requests, responses, and inbound/outbound contracts.

- **Exception**
  Centralizes custom exceptions and structures related to application error handling.

#### Tests

Contains the projects responsible for validating the application behavior at different levels.

- **UseCases.Tests**
  Contains tests focused on the application use cases, validating rules, workflows, and behaviors of the Application layer.

- **Validators.Tests**
  Contains specific tests for application validations, ensuring that input data is correctly evaluated.

- **WebApi.Tests**
  Contains API integration tests, validating the real behavior of the API, including database and application configurations.

## CI/CD and code quality

The project uses **Azure DevOps Pipelines** to automate important development steps, such as:

- Package restore
- Solution build
- Test execution
- Test coverage publishing
- Docker image publishing with the API to Azure Container Registry

## How to run the project

To run the project locally, follow the steps below.

### Prerequisites

- Visual Studio 2026 or Rider
- .NET SDK
- Docker Desktop
- MySQL Server or SQL Server
- Git

---

<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/en-us/download/dotnet/10.0
[curso-udemy]: https://www.udemy.com/course/net-core-curso-orientado-para-mercado-de-trabalho/?referralCode=C0850BF224055DE39722

<!-- Badges -->
[badge-c#]: https://custom-icon-badges.demolab.com/badge/C%23-%23239120.svg?logo=cshrp&logoColor=white&style=for-the-badge
[badge-sqlserver]: https://custom-icon-badges.demolab.com/badge/SQL%20Server-CC2927?logo=mssqlserver-white&logoColor=white&style=for-the-badge
[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-windows]: https://custom-icon-badges.demolab.com/badge/Windows-0078D6?logo=windows11&logoColor=white&style=for-the-badge
[badge-visual-studio]: https://custom-icon-badges.demolab.com/badge/Visual%20Studio-5C2D91.svg?&logo=visualstudio&logoColor=white&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge
[badge-docker]: https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=fff&style=for-the-badge
[badge-azure-devops]: https://custom-icon-badges.demolab.com/badge/Azure%20DevOps-0078D7?logo=azure-devops-white&logoColor=fff&style=for-the-badge
[badge-azure]: https://custom-icon-badges.demolab.com/badge/Microsoft%20Azure-0089D6?logo=msazure&logoColor=white&style=for-the-badge
[badge-gmail]: https://img.shields.io/badge/Gmail-D14836?logo=gmail&logoColor=white&style=for-the-badge
[badge-openai]: https://custom-icon-badges.demolab.com/badge/OpenAI-74aa9c?logo=openai&logoColor=white&style=for-the-badge
[badge-yaml]: https://img.shields.io/badge/YAML-CB171E?logo=yaml&logoColor=fff&style=for-the-badge
