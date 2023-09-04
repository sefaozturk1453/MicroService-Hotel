
# Otel Raporlama

Birbirleri ile haberleşen minimum iki microservice'in olduğu bir yapıda, basit
bir telefon rehberi uygulaması.

ConnectionStrings bilgisi appsettings.json içinde tutulup gerekli tabloların veritabanına aktarımı için ayarlanmalıdır.

Otel işlemleri senkron çalışırken.
Rapor işlemleri asenkron olarak çalışmaktadır.

Proje iki adet Web Api nin multiple startup olarak ayarlanarak açılmalıdır.





![Uygulama Ekran Görüntüsü](https://www.linkpicture.com/q/Ekran-goruntusu-2023-08-29-132200.png)

  
## Teknolojiler

**Kullanılan Teknolojiler:** .NET Core (5.0), Postgres(Npgsql.EntityFrameworkCore.PostgreSQL), RabbitMq(MassTransit.AspNetCore, MassTransit.RabbitMQ)

**Kullanılan Kütüphaneler:** Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design ,Microsoft.EntityFrameworkCore.Tools , Npgsql.EntityFrameworkCore.PostgreSQL, MassTransit.AspNetCore, MassTransit.RabbitMQ ,Swashbuckle.AspNetCore

  
## API Kullanımı

https://localhost:5001/swagger/index.html

#### Otel ekle

```http
  POST /api/Directory/AddHotel
```

| Parametre -Tip     | Açıklama                |
| :--------  | :------------------------- |
| `{
  "Name": "string"
}`  | **Gerekli**. API anahtarınız. |

#### Otel sil

```http
  DELETE /api/Directory/DeleteHotel/${guid}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `guid`      | `guid` | **Gerekli**. Çağrılacak öğenin anahtar değeri |


#### Otel için iletişim bilgisi ekle

```http
  POST /api/Directory/AddHotelContact
```

| Parametre -Tip     | Açıklama                |
| :--------  | :------------------------- |
| `{
  "Id": "b2303eea-4e5b-4ed3-9e80-5d524bbbdd3d",
  "type": 0,
  "info": "string"
}`  | **Gerekli**. API anahtarınız. |


#### Otel iletişim bilgisini sil

```http
  DELETE /api/Directory/DeleteHotelContact/${guid}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `guid`      | `guid` | **Gerekli**. Çağrılacak öğenin anahtar değeri |


#### Yetkili kişileri getir.

```http
  GET /api/Directory/GetOfficialList
```

#### Otellerin iletişim bilgisini getir

```http
  GET /api/Directory/GetHotelContactList/${guid}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `guid`      | `guid` | **Gerekli**. Çağrılacak öğenin anahtar değeri |

#### Rapor oluştur.

```http
  GET /api/Directory/GetLocationReport
```

#### Raporları getir.

```http
  GET /api/Directory/GetReportList
```

#### Rapor ayrıntısını getir.

```http
  GET /api/Directory/GetPersonContactList/${guid}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `guid`      | `guid` | **Gerekli**. Çağrılacak öğenin anahtar değeri |




  
