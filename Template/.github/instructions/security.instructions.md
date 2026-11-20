---
name: 'Security Guidelines'
description: 'Security requirements for .NET development.'
applyTo: 'src/**/*.cs'
---

# Security Guidelines

## 1. General Security Principles

- Follow secure coding practices.
- Apply the principle of least privilege.
- Validate all untrusted input.
- Never trust data received from external sources.
- Avoid exposing sensitive information.

## 2. Input Validation

- Validate incoming requests.
- Validate data according to business requirements.
- Avoid trusting client-provided identifiers or permissions.
- Protect against injection vulnerabilities.
- Use parameterized queries.

## 3. Sensitive Information

- Never hardcode passwords, API keys or connection strings containing secrets.
- Use secure configuration providers.
- Avoid logging credentials, tokens or personal sensitive information.
- Do not expose internal exception details through API responses.

## 4. Database Security

- Use parameterized database operations.
- Avoid dynamically concatenating SQL queries.
- Apply least privilege to database accounts.
- Avoid exposing unnecessary database information.

## 5. API Security

- Validate request payloads.
- Apply appropriate authorization policies.
- Avoid mass assignment vulnerabilities.
- Implement appropriate rate limiting when required.
- Avoid exposing internal implementation details.

## 6. Dependency Security

- Avoid unnecessary third-party packages.
- Prefer maintained and trusted dependencies.
- Review dependency vulnerabilities.
- Do not introduce packages with known critical vulnerabilities without an approved mitigation.

## 7. Security Review

Before completing a change:

1. Identify potential attack surfaces.
2. Review input validation.
3. Review authentication and authorization.
4. Check for sensitive information exposure.
5. Evaluate dependency and configuration risks.