---
name: 'C# Coding Standards'
description: 'Coding conventions and implementation standards for C#.'
applyTo: 'src/**/*.cs'
---

# C# Coding Standards

## 1. Naming Conventions

- Use PascalCase for classes, records, interfaces, methods and properties.
- Use camelCase for local variables and method parameters.
- Prefix interfaces with I.
- Use meaningful and descriptive names.
- Avoid abbreviations unless they are widely understood.

## 2. C# Language Features

- Prefer modern C# language features when appropriate.
- Use nullable reference types.
- Prefer records for immutable data structures when appropriate.
- Use pattern matching when it improves readability.
- Use collection expressions when supported by the project version.
- Avoid unnecessary use of dynamic.

## 3. Methods and Classes

- Keep methods small and focused.
- Each class should have a clear responsibility.
- Avoid methods with excessive parameters.
- Avoid unnecessary static dependencies.
- Prefer composition over inheritance when appropriate.
- Avoid premature abstractions.

## 4. Asynchronous Programming

- Use async and await for I/O-bound operations.
- Use CancellationToken in asynchronous operations when appropriate.
- Propagate CancellationToken through dependent calls.
- Avoid async void except for event handlers.
- Do not block asynchronous operations using .Result or .Wait().
- Do not use Task.Run to wrap naturally asynchronous I/O operations.

## 5. Dependency Injection

- Use constructor injection.
- Avoid service locator patterns.
- Register services with the appropriate lifetime.
- Do not inject dependencies that are not required.

## 6. Error Handling

- Use specific exception types.
- Do not swallow exceptions.
- Avoid using exceptions for normal business flow.
- Preserve the original exception when rethrowing.
- Return meaningful error information without exposing internal details.

## 7. Code Maintainability

- Avoid duplicated code.
- Avoid deeply nested conditionals.
- Prefer guard clauses when appropriate.
- Avoid unnecessary comments.
- Keep methods readable.
- Follow the existing formatting conventions.

## 8. Configuration

- Do not hardcode environment-specific values.
- Use IConfiguration and the Options pattern when appropriate.
- Never store credentials or secrets in source code.