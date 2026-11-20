# Project Instructions

## 1. Project Overview

This repository contains a .NET application developed using C# and
ASP.NET Core.

The application follows a modular and maintainable architecture
with a strong separation of concerns.

## 2. Technology Stack

- Language: C#
- Framework: .NET 10
- Web Framework: ASP.NET Core
- ORM: Entity Framework Core
- Database: SQL Server
- Testing: xUnit
- Mocking: Moq
- API Documentation: Swagger / OpenAPI

## 3. General Development Principles

All implementations must follow these principles:

- SOLID principles.
- Separation of concerns.
- DRY (Don't Repeat Yourself).
- KISS (Keep It Simple, Stupid).
- Explicit dependencies.
- High cohesion and low coupling.
- Maintainable and testable code.

## 4. Development Guidelines

- Follow the existing project structure.
- Reuse existing components whenever possible.
- Avoid unnecessary dependencies.
- Do not introduce architectural changes without justification.
- Prefer asynchronous operations for I/O-bound operations.
- Use dependency injection.
- Avoid unnecessary abstractions.
- Keep implementations simple and focused.

## 5. Code Quality

- Write readable and maintainable code.
- Use meaningful names for classes, methods and variables.
- Avoid duplicated logic.
- Handle exceptions appropriately.
- Do not swallow exceptions.
- Avoid hardcoded configuration values.
- Do not introduce compiler warnings without justification.

## 6. Working with Existing Code

Before implementing changes:

1. Inspect the existing project structure.
2. Identify the affected components.
3. Understand existing dependencies.
4. Follow established conventions.
5. Identify potential side effects.

Do not assume that a new implementation is required
when an existing component can be reused.

## 7. Documentation

- Keep documentation synchronized with implementation.
- Document complex business rules.
- Use XML documentation when appropriate for public APIs.
- Avoid unnecessary comments that repeat the code.

## 8. Important Restrictions

- Do not modify unrelated files.
- Do not introduce new NuGet packages without justification.
- Do not change public contracts without considering compatibility.
- Do not expose secrets or credentials.
- Do not invent requirements that have not been provided.

# AGENTS.md — Mapa de navegación para agentes de IA

> Este archivo es el **punto de entrada** para cualquier agente que trabaje en este
> repositorio. NO es una biblia de reglas: es un **mapa**. Lee solo lo que
> necesites cuando lo necesites (divulgación progresiva).

---
