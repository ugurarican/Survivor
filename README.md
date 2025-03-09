# Survivor Yarışması Web API Projesi

Bu proje, Survivor yarışması için bir Web API uygulamasıdır. Yarışmacılar ve kategoriler arasında ilişkiler kurar ve bu ilişkilerle ilgili CRUD (Create, Read, Update, Delete) işlemlerini gerçekleştiren API endpoint'leri sunar.

## Proje Amacı

- Yarışmacıların ve kategorilerin yönetimi.
- Yarışmacıların belirli kategorilere atanması.
- Soft delete (yumuşak silme) özelliği ile veri kaybını önleme.
- RESTful API standartlarına uygun endpoint'ler sunma.

## Kullanılan Teknolojiler

- **.NET 8**: Web API geliştirme platformu.
- **Entity Framework Core**: ORM (Object-Relational Mapping) aracı.
- **SQL Server**: Veritabanı yönetim sistemi.
- **Swagger**: API dokümantasyonu ve test arayüzü.
- **LINQ**: Veritabanı sorguları için kullanılan dil.

## Kurulum Adımları

### 1. Gereksinimler

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) veya [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/tr-tr/sql-server/sql-server-downloads)

### 2. Projeyi Klonlama

Projeyi bilgisayarınıza klonlamak için aşağıdaki komutu kullanın:

```bash
git clone https://github.com/kullanici-adiniz/survivor-api.git
cd survivor-api
```

### 3. Veritabanı Ayarları

1. SQL Server'da yeni bir veritabanı oluşturun (örneğin, `Survivor`).
2. `appsettings.json` dosyasındaki bağlantı dizesini güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SUNUCU_ADI;Database=Survivor;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 4. Migration'ları Uygulama

Veritabanını oluşturmak ve güncellemek için aşağıdaki komutları çalıştırın:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Projeyi Çalıştırma

Projeyi çalıştırmak için aşağıdaki komutu kullanın:

```bash
dotnet run
```

## API Endpoint'leri

### Kategoriler (Categories)

- **Tüm Kategorileri Listele**: `GET /api/categories`
- **Belirli Bir Kategoriyi Getir**: `GET /api/categories/{id}`
- **Yeni Kategori Ekle**: `POST /api/categories`
- **Kategori Güncelle**: `PUT /api/categories/{id}`

### Yarışmacılar (Competitors)

- **Tüm Yarışmacıları Listele**: `GET /api/competitors`
- **Belirli Bir Yarışmacıyı Getir**: `GET /api/competitors/{id}`
- **Kategoriye Göre Yarışmacıları Listele**: `GET /api/competitors/categories/{categoryId}`
- **Yeni Yarışmacı Ekle**: `POST /api/competitors`
- **Yarışmacı Güncelle**: `PUT /api/competitors/{id}`
- **Yarışmacı Sil**: `DELETE /api/competitors/{id}`

## Görseller

![db](https://github.com/ugurarican/Survivor/blob/master/survivor-db.png)
![OneToMany](https://github.com/ugurarican/Survivor/blob/master/survivor-oneToMany.png)
![swagger](https://github.com/ugurarican/Survivor/blob/master/survivor-swagger.png)
