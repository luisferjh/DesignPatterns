---
name: 'Testing Guidelines'
description: 'Testing conventions for .NET applications.'
applyTo: 'tests/**/*.cs'
---

# Testing Guidelines

## 1. Testing Framework

- Use xUnit as the testing framework.
- Use Moq for mocking dependencies when appropriate.
- Use FluentAssertions if it is already available in the project.
- Follow the existing testing conventions.

## 2. Testing Principles

- Follow the Arrange, Act, Assert pattern.
- Tests must be independent.
- Avoid shared mutable state between tests.
- Each test should verify a specific behavior.
- Do not test implementation details unnecessarily.

## 3. Naming Conventions

Use descriptive test names.

Recommended format:

MethodName_Scenario_ExpectedResult

Example:

CreateUser_WithValidData_ReturnsCreatedUser

## 4. Unit Testing

- Test business rules independently.
- Mock external dependencies.
- Avoid accessing real databases in unit tests.
- Test successful and failure scenarios.
- Include boundary conditions.
- Verify important interactions when necessary.

## 5. Integration Testing

- Use integration tests to validate component interactions.
- Test API endpoints using WebApplicationFactory when appropriate.
- Use isolated test databases or database containers when necessary.
- Avoid depending on production data.
- Ensure test resources are properly disposed.

## 6. Test Coverage

Consider the following scenarios:

- Valid input.
- Invalid input.
- Null values.
- Boundary conditions.
- Expected exceptions.
- External dependency failures.

## 7. Test Execution

After modifying production code:

1. Identify affected tests.
2. Run relevant unit tests.
3. Run relevant integration tests when necessary.
4. Investigate failures.
5. Do not modify tests simply to make them pass without understanding the failure.

## 8. Test Quality

- Avoid meaningless assertions.
- Avoid excessive mocking.
- Do not duplicate production logic inside tests.
- Keep tests readable and maintainable.