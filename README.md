# OmniHub - Multi-Tenant B2B SaaS Platform

OmniHub is a B2B e-commerce inventory management infrastructure built from scratch with modern software architecture principles and multi-tenant support.

## 🚀 Project Vision
This project was built to demonstrate how "clean architecture" and "data isolation" concepts can be applied in the real world by an independent developer. The system uses JWT-based authentication on a single database while keeping data from different companies (tenants) 100% isolated.

## 🏗️ Technologies & Architecture
* **Backend:** .NET 10 (C#), ASP.NET Core Web API
* **Frontend:** Blazor WebAssembly
* **Database:** PostgreSQL (Entity Framework Core)
* **Architecture:** Onion Architecture (Domain, Application, Infrastructure, Presentation)
* **Infrastructure & Orchestration:** Docker & Docker Compose
* **Network Management:** Nginx (Reverse Proxy)

## 🔥 Key Features
* **True Multi-Tenancy:** Each user is registered with their own `TenantId`, and data isolation is enforced through claims in JWT.
* **Containerized Infrastructure:** API, Blazor Client, and Database communicate over dedicated Docker networks with a healthcheck-driven startup sequence.
* **Webhook Simulation:** A subscription endpoint that captures successful payment signals from external payment systems (Stripe/Iyzico, etc.) and automatically creates a Tenant (Company).

## ⚙️ How to Run
To run the project on your machine, having Docker installed is enough.

1. Clone or download the repository to your computer.
2. Open a terminal and go to the project root directory.
3. Run the command below:
   ```bash
   docker-compose up -d --build