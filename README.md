# OmniHub - Multi-Tenant B2B SaaS Platform

OmniHub, modern yazılım mimarisi prensipleriyle sıfırdan geliştirilmiş, çoklu kiracı (multi-tenant) destekli bir B2B e-ticaret stok yönetimi altyapısıdır.

## 🚀 Proje Vizyonu
Bu proje, bağımsız bir geliştirici olarak "temiz mimari" (Clean Architecture) ve "veri izolasyonu" kavramlarının gerçek dünyada nasıl uygulanacağını göstermek amacıyla inşa edilmiştir. Sistem, tek bir veritabanı üzerinde JWT tabanlı kimlik doğrulama ile farklı şirketlerin (tenant) verilerini birbirinden %100 izole eder.

## 🏗️ Kullanılan Teknolojiler & Mimari
* **Backend:** .NET 10 (C#), ASP.NET Core Web API
* **Frontend:** Blazor WebAssembly
* **Veritabanı:** PostgreSQL (Entity Framework Core)
* **Mimari:** Onion Architecture (Domain, Application, Infrastructure, Presentation)
* **Altyapı & Orkestrasyon:** Docker & Docker Compose
* **Ağ Yönetimi:** Nginx (Reverse Proxy)

## 🔥 Öne Çıkan Özellikler
* **Gerçek Multi-Tenancy:** Her kullanıcının kendi `TenantId`'si ile sisteme kayıt olması ve JWT içindeki claim'ler ile veri izolasyonunun sağlanması.
* **Konteynerize Altyapı:** API, Blazor Client ve Veritabanının kendi özel Docker ağlarında (custom network) haberleştiği, `healthcheck` kontrollü hatasız başlangıç dizilimi.
* **Webhook Simülasyonu:** Dış ödeme sistemlerinden (Stripe/İyzico vb.) gelebilecek başarılı ödeme sinyallerini yakalayıp otomatik Tenant (Şirket) oluşturan abonelik uç noktası.

## ⚙️ Nasıl Çalıştırılır?
Projeyi kendi bilgisayarınızda ayağa kaldırmak için sisteminizde Docker'ın yüklü olması yeterlidir.

1. Repoyu bilgisayarınıza indirin.
2. Terminali açıp projenin ana dizinine gidin.
3. Aşağıdaki komutu çalıştırın:
   ```bash
   docker-compose up -d --build