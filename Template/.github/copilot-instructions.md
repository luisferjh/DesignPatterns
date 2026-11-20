# copilot-instructions.md — Mapa de navegación para agentes de IA

> Este archivo es el **punto de entrada** para cualquier agente que trabaje en este
> repositorio. NO es una biblia de reglas: es un **mapa**. Lee solo lo que
> necesites cuando lo necesites (divulgación progresiva).

---

## 1. Mapa del repositorio

| Archivo / carpeta            | Qué contiene                                              | Cuándo leerlo |
|------------------------------|-----------------------------------------------------------|---------------|
| `instructions/general.instructions.md`       | Información general sobre el proyecto                     | Al iniciar en el proyecto |
| `instructions/architecture.instructions.md`       | Qué significa "hacer un buen trabajo" en este proyecto    | Antes de implementar |
| `instructions/coding-standards.instructions.md`        | Reglas de estilo, nombres, estructura                     | Antes de escribir código |
| `instructions/security.instructions.md`       | Cómo mantener la seguridad en el proyecto                | Antes de crear una feature |
| `instructions/testing.instructions.md`       | Cómo escribir y ejecutar tests                             | Antes de crear tests |
| `.github/agents/`            | Definiciones de subagentes (backend-developer.agent) | Si orquestas trabajo |
| `src/`                       | Código de la aplicación                                   | Para implementar |
| `tests/`                     | Tests automáticos                                         | Para verificar |

## 2. Reglas duras (no negociables)

- **Una sola feature a la vez.** No mezcles cambios de varias tareas en la misma sesión.
- **Deja el repositorio limpio** antes de cerrar la sesión.
- **Si no sabes algo, busca en `instructions/`** antes de inventarlo.

## 3. Si te bloqueas

- Relee la sección relevante de `instructions/`.
- Si la herramienta no hace lo que esperas, **no inventes un workaround**.