# Shared Finance Manager 🧾💰

A personal project focused on practicing **software architecture**, **Domain-Driven Design (DDD)**, **CQRS**, and **clean code principles** in C#, evolving step by step from a simple console application into a full-featured web application.

The main goal is not only to solve a financial problem, but to **learn through the system’s evolution**, applying architectural patterns incrementally and documenting decisions along the way.

---

## 🎯 Project Goal

Build a shared personal finance management system where:

- Users have accounts
- Expenses can be split between users
- One user can owe money to another (receivable)
- Transfers can be performed
- Balance is calculated from a transaction ledger
- The system evolves through technical phases

---

## 🧠 Concepts Applied

This project explores and applies:

- Domain-Driven Design (DDD)
  - Aggregates
  - Entities
  - Value Objects
  - Ubiquitous Language
  - Domain Rules
- CQRS (Command Query Responsibility Segregation) – light version
- Command Pattern
- Mediator Pattern (custom implementation)
- Repository Pattern
- Clean Architecture (layered separation)
- Unit Testing
- Incremental architectural evolution

---

## 🔁 Evolution by Phases

### ✅ Phase 1 — Console + DDD + CQRS Light
- Console application
- Rich domain model (rules inside aggregates)
- Financial ledger (balance derived from transactions)
- Commands and Queries
- Custom mediator
- In-memory repositories
- Unit tests
- Menu using Command Pattern

### 🔜 Phase 2 — Desktop + Persistence
- Graphical UI (WPF or WinUI)
- Database (SQLite / EF Core)
- Dependency Injection
- Logging
- Validation
- Reports

### 🔜 Phase 3 — Web API
- ASP.NET Web API
- REST endpoints
- DTOs
- Authentication (JWT)
- Swagger
- Full CQRS
- Possible separation into Bounded Contexts

---

## 💡 Domain Concepts

- **Deposit**: money already received
- **Receivable**: money to be received (debt from another user)
- **Expense**: outgoing money
- **Transfer**: movement between accounts
- **Ledger**: transaction history
- **Balance**: calculated from the ledger

The balance is never stored directly — it is always derived from transactions.

---

## 🧪 Testing

Tests focus on:

- Domain rules
- Aggregate behavior
- Command Handlers
- Query Handlers

Using:
- xUnit
- NSubstitute

Structure:

SharedFinance.Domain.Tests
SharedFinance.Application.Tests


---

## 🚀 How to Run

Build the project:

```
dotnet build
```

Run the console app:

```
dotnet run --project src/SharedFinance.ConsoleUI
```

Run tests:

```
dotnet test
```

## 🗺️ Roadmap

- [x] Layered architecture
- [x] Domain + Aggregates
- [x] CQRS light
- [x] Mediator pattern
- [x] Domain unit tests
- [ ] Database persistence (EF Core / SQLite)
- [ ] Desktop UI (WPF or WinUI)
- [ ] REST API (ASP.NET Web API)
- [ ] Authentication (JWT)
- [ ] Bounded Contexts
- [ ] Observability (logging & metrics)
- [ ] Docker support
- [ ] Cloud deployment (future)

---

## 📚 Motivation

This project exists to:

- Practice software architecture in real-world scenarios  
- Apply Domain-Driven Design (DDD) and CQRS concepts  
- Build a strong technical portfolio  
- Learn through incremental system evolution  
- Experiment with clean architecture and design patterns  
- Document architectural decisions and trade-offs  

---

## 👨‍💻 Author

Chrystian Amaral  
Personal project for learning and architectural practice in C#.

---

## 📜 License

MIT License
