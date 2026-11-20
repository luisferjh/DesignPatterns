---
name: 'Architecture Guidelines'
description: 'Architectural rules for .NET applications.'
applyTo: 'src/**/*.cs'
---

# Architecture Guidelines

## 1. Architectural Principles

- Follow the architecture already established in the solution.
- Maintain separation of concerns.
- Apply Dependency Inversion Principle.
- Keep business logic independent of infrastructure concerns.
- Avoid circular dependencies.
- Maintain high cohesion and low coupling.

## 2. Project Organization

Organize code according to the responsibilities of each project.

Typical responsibilities:

- API: HTTP endpoints, controllers and request handling.
- Application: Use cases, business orchestration and interfaces.
- Domain: Business entities, value objects and domain rules.
- Infrastructure: Database access, external services and integrations.

Do not introduce these layers if the existing solution
uses a different architecture.

## 3. Dependency Management

- Dependencies must point toward the appropriate architectural layers.
- Domain must not depend on Infrastructure.
- Application must not depend directly on implementation details.
- Use interfaces to define contracts where abstraction is justified.
- Register dependencies through the dependency injection container.

## 4. Business Logic

- Keep business rules out of controllers.
- Avoid placing business logic inside repositories.
- Keep application use cases focused on one responsibility.
- Avoid anemic abstractions that add no real value.
- Validate business invariants in the appropriate layer.

## 5. Data Access

- Use Entity Framework Core for database operations.
- Keep database-specific logic inside Infrastructure.
- Avoid exposing DbContext outside the data access boundary.
- Use asynchronous database operations.
- Avoid unnecessary database queries.
- Pay attention to N+1 query problems.
- Use a In memory database if is not necessary to use a real database.
- Ensure the in-memory database is properly configured for testing scenarios.

## 6. API Layer

- Controllers or endpoints must remain thin.
- Delegate business operations to the Application layer.
- Use DTOs for API contracts.
- Do not expose database entities directly.
- Return appropriate HTTP status codes.
- Validate incoming requests.

## 7. Architecture Changes

Before introducing a new architectural component:

1. Inspect the existing architecture.
2. Identify the problem being solved.
3. Evaluate existing abstractions.
4. Consider the impact on dependencies.
5. Prefer the simplest solution that satisfies the requirement.