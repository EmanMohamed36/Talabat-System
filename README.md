# Talabat System 🍔

## Table of Contents 📚
- [About](#about-)
- [Technologies](#technologies-)
- [Architecture](#architecture-)
- [ProductionDeployment](#ProductionDeployment-)
- [APIUsage](#APIUsage-)
- [AngularRepo](#AngularRepo-)
- [Demo](#demo-)



---

## About 📝
The Talabat System is a full-stack food delivery platform that allows users to easily order their favorite meals online:
- User Registration & Login with secure **JWT authentication**.
- Browse & Search Products: View all available food items, filter by category, and search for specific dishes.
- Shopping Basket (Cart) stored efficiently using **Redis** for fast access
- Order Food & Payment: Add items to the cart, place orders, and pay securely using a card through **Stripe Payment Gateway**.
- Clean Architecture & Best Practices:
    - Dependency Injection (DI) for flexible and testable components.
    - Repository & Unit of Work Patterns for efficient data management.
    - Service Manager Layer to organize business logic.
- User-Friendly Interface: A clean, responsive frontend built with Angular for smooth navigation.


## Technologies 🛠️

- Backend: ASP.NET Core, RESTful API, Entity Framework Core.
- Frontend: Angular, TypeScript, Bootstrap.
- Architecture: MC + Onion Layer.
- Database: SQL Server.
- Tools: Visual Studio, Postman


## Architecture 🏗️
The backend uses **ASP.NET Core** with **Onion Architecture**:

    Presentation Layer (Controllers)
    
    ↓
    
    Application Layer (Services / Use Cases)
    
    ↓
    
    Domain Layer (Entities, Interfaces)
    
    ↓
    
    Infrastructure Layer (Database, Repositories)

## ProductionDeployment 🌐

- Backend Deployment:
  - The ASP.NET Core REST API is hosted on **Monster ASP.NET** hosting with full HTTPS support, ensuring secure communication across all endpoints.
  - Link -> https://talabatsystem.runasp.net

- Redis (Basket Storage):
  - The shopping basket is stored in **Redis Upstash**, a fully managed cloud Redis service, allowing ultra-fast retrieval with minimal latency and zero maintenance.

- Stripe Payment Integration:
  - Payments in production are handled through Stripe Checkout, and the system uses a Stripe Webhook endpoint to verify payment success, maintain secure order completion, and prevent fraudulent or incomplete
      transactions.

## APIUsage 🚀
- Base URL -> https://talabatsystem.runasp.net
- 📦 Products Endpoints:
  - GET {{BaseUrl}}/api/Products?BrandId=1&sortingOptions=PriceDesc&TypeId=3&SearchValue=Tandoori&PageSize=5&PageIndex=2
  - GET {{BaseUrl}}/api/Products/12

## [AngularRepo](https://github.com/EmanMohamed36/ClientSide-Of-TalabatSystem) 💻

## Demo 🎬 
![Talabat System Demo](https://github.com/EmanMohamed36/Talabat-System/blob/main/TalabatVedio-ezgif.com-video-to-gif-converter%20(1).gif)  
[Click here to show video](https://drive.google.com/file/d/1FWUqXc-3DBSP35lFpMuWuD6p0N9_m1AY/view?usp=sharing)  

