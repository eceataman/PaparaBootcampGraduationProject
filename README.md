# 📊 Papara Expense Tracking System

## 🎯 Proje Amacı

Bu proje, bir şirketin sahada çalışan personelinin masraf kalemlerini hızlıca sisteme girmesini, yöneticilerin bu masrafları onaylamasını veya reddetmesini, onaylanan talepler için ödeme simülasyonu yapılmasını ve tüm sürecin güvenli, rol bazlı ve raporlanabilir şekilde yönetilmesini sağlar.
Projeye dair ekran görüntüleri için:
https://drive.google.com/drive/folders/1dMWH-wJh0zfstGbY6xsV--FTffu-Lfqb?usp=drive_link

## ⚙️ Özellikler

- 🔐 JWT tabanlı kimlik doğrulama ve rol bazlı yetkilendirme (Admin / Personel)
- 🧾 Masraf oluşturma, görüntüleme, onaylama, reddetme
- 🗂 Kategori işlemleri (Sadece Admin tarafından yönetilebilir)
- 📊 Gelişmiş raporlar (Dapper + View destekli):
  - Kendi hareketlerini görme
  - Günlük / Haftalık / Aylık masraf yoğunluğu
  - Red / Onay istatistikleri
- 🧪 Swagger üzerinden uçtan uca test edilebilir API'ler
- 📥 Fatura/fotoğraf yükleme (file upload)
- 📌 Validasyonlar ve Exception kontrolü

---

## 🧱 Katmanlar

- `Domain`: Varlıklar ve enumlar
- `Persistence`: EF Core DbContext ve Fluent API konfigürasyonları
- `API`: Controller, Service, DTO ve Swagger konfigürasyonları

---

## 🧪 Kullanılan Teknolojiler

| Teknoloji           | Açıklama                          |
|---------------------|-----------------------------------|
| ASP.NET Core Web API| Backend geliştirme                |
| EF Core             | ORM ve DB işlemleri               |
| Dapper              | Performans odaklı SQL işlemleri   |
| MS SQL Server       | Veritabanı                        |
| Swagger (Swashbuckle)| API test ve dökümantasyon       |
| JWT                 | Kimlik doğrulama                  |

---

## 🚀 Kurulum

1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/kullaniciAdi/Papara.ExpenseTrackingSystem.git
2.appsettings.json → DefaultConnection alanını kendi SQL Server bağlantınıza göre güncelleyin.
3.Migration oluştur:
Add-Migration InitialCreate -StartupProject Papara.ExpenseTrackingSystem.API -Project Persistence
Update-Database
4.Uygulamayı çalıştır:
dotnet run --project Papara.ExpenseTrackingSystem.API

## Swagger Kullanımı 
Giriş yap (Login):
POST /api/Auth/login
Token'ı kopyalayın
Bearer <token>
Artık tüm [Authorize] endpoint'leri test edilebilir.
## Rol	Açıklama
Admin->	Tüm masrafları yönetir, kategorileri değiştirir, kullanıcıları yönetir
Personel	->Sadece kendi masraflarını ekler/görüntüler

## Örnek Kullanıcılar (Initial Seed)
[
  {
    "Email": "admin@mail.com",
    "Password": "admin123",
    "Role": "Admin"
  },
  {
    "Email": "personel@mail.com",
    "Password": "personel123",
    "Role": "Personel"
  }
]

Masraf CRUD + validasyonlar, Kategori CRUD (Admin-only), Kullanıcı yönetimi (Admin-only),Raporlar (Dapper + View),JWT Auth + Role-based kontrol,Swagger test arayüzü, Exception ve edge case kontrolleri
