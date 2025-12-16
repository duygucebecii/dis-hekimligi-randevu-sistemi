# 🦷 Diş Hekimi Randevu Sistemi

Bu proje, diş hekimleri ve hastalar arasında randevu yönetimini sağlayan  
**ASP.NET Core MVC tabanlı bir web uygulamasıdır**.

Sistem sayesinde hastalar ve diş hekimleri tanımlanabilir, randevular
oluşturulabilir, listelenebilir, güncellenebilir ve silinebilir.

---

## 🚀 Kullanılan Teknolojiler
- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- HTML
- CSS
- Bootstrap

---

## ⚙️ Sistem Özellikleri
- Kullanıcı giriş sistemi
- Hasta yönetimi
- Diş hekimi yönetimi
- Randevu oluşturma
- Randevu listeleme
- Randevu güncelleme
- Randevu silme
- MVC mimarisi kullanımı

---

## 🗂 Proje Yapısı

Controllers/
├── AppointmentsController
├── DentistsController
├── PatientsController
├── LoginController

Models/
├── Appointment
├── Dentist
├── Patient
├── User
├── ApplicationDbContext

yaml
Kodu kopyala

---

## 🛠 Kurulum

1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/duygucebecii/dis-hekimligi-randevu-sistemi.git
Visual Studio ile projeyi açın

appsettings.json dosyasında kendi SQL Server bağlantınızı tanımlayın

Entity Framework migration işlemlerini uygulayın:

bash
Kodu kopyala
add-migration InitialCreate
update-database
Projeyi çalıştırın
