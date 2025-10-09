# Fintrack

![GitHub last commit](https://img.shields.io/github/last-commit/gabbium/fintrack)
![Sonar Quality Gate](https://img.shields.io/sonar/quality_gate/gabbium_fintrack?server=https%3A%2F%2Fsonarcloud.io)
![Sonar Coverage](https://img.shields.io/sonar/coverage/gabbium_fintrack?server=https%3A%2F%2Fsonarcloud.io)

---

## 📌 About

**Fintrack** is a **microservices-based personal finance system** built with **.NET 9**,
designed for tracking expenses, managing income, and generating financial insights.

It follows **Clean Architecture**, **CQRS**, and **DDD** principles — with full observability using **OpenTelemetry** and the **.NET Aspire dashboard**.

---

## 🏗️ Architecture

Fintrack runs on **Azure Container Apps**, within a shared environment, using a shared **PostgreSQL Flexible Server** and **Keycloak** for authentication.

Main components:

- **Ledger API** – handles income and expense tracking
- **Ledger Migration Service** – applies EF Core migrations during deployment for **ledgerdb**
- **Planning API** – handles financial planning and scheduled transactions
- **Planning Migration Service** – applies EF Core migrations during deployment for **planningdb**
- **Aspire Dashboard** – provides observability (logs, metrics, traces)

---

## 💻 Development

Local development is handled entirely through **.NET Aspire**: running the solution automatically starts Keycloak, PostgreSQL, the API, the migration service, and opens the Aspire dashboard — no manual setup required.

You can also scaffold new services or use cases via Fintrack templates:

- `dotnet new fintrack-service`
- `dotnet new fintrack-usecase`

---

## 🚀 Workflows

Fintrack uses **GitHub Actions** for continuous integration and delivery:

| Workflow                 | Purpose                                                                      |
| ------------------------ | ---------------------------------------------------------------------------- |
| **Backend — Validate**   | Validates code formatting, runs SonarCloud analysis, and Docker build checks |
| **Terraform — Validate** | Checks Terraform syntax, formatting, and dry-run plan                        |
| **Release & Versioning** | Generates semantic version tags and GitHub releases                          |
| **Publish & Deploy**     | Builds Docker images and applies Terraform to update Azure containers        |

---

## 🪪 License

This project is licensed under the **MIT License** — see [LICENSE](LICENSE) for details.
