# KutuphaneYonetimSistemiDb
C# ve SQL Server ile kütüphane yönetim sistemi, kitap, üye ve ödünç takibi.
# Kütüphane Yönetim Sistemi

SQL Server üzerinde kurulmuş bir kütüphane veritabanı projesi. Kitapları, üyeleri ve ödünç verme kayıtlarını tutar. İlerleyen aşamada C# konsol uygulamasıyla yönetilecek.

## Durum

- [x] Veritabanı tasarımı
- [x] Tabloların ve ilişkilerin oluşturulması
- [x] Deneme verileriyle test
- [ ] C# konsol uygulaması (sırada)

## Veritabanı: KutuphaneDb

Üç tablo var:

### Books (Kitaplar)
| Sütun | Açıklama |
|---|---|
| BookId | Kitap kimliği (birincil anahtar, otomatik artan) |
| Title | Kitabın adı |
| Author | Yazarı |
| Category | Türü / kategorisi |
| PublicationYear | Basım yılı |
| IsAvailable | Kitap rafta mı (1) ödünçte mi (0) |

### Members (Üyeler)
| Sütun | Açıklama |
|---|---|
| MemberId | Üye kimliği (birincil anahtar, otomatik artan) |
| FirstName | Ad |
| LastName | Soyad |
| Phone | Telefon (metin olarak tutulur) |
| Email | E-posta |
| MembershipDate | Üyelik tarihi |
| IsActive | Üyelik aktif mi |

### Loans (Ödünç Kayıtları)
| Sütun | Açıklama |
|---|---|
| LoanId | Kayıt kimliği (birincil anahtar, otomatik artan) |
| BookId | Ödünç verilen kitap (Books tablosuna yabancı anahtar) |
| MemberId | Kitabı alan üye (Members tablosuna yabancı anahtar) |
| LoanDate | Ödünç alınma tarihi |
| DueDate | Son teslim tarihi |
| ReturnDate | İade tarihi (iade edilmediyse boş) |

## Tablo İlişkileri

- Bir kitap birden fazla ödünç kaydında yer alabilir (Books → Loans).
- Bir üyenin birden fazla ödünç kaydı olabilir (Members → Loans).
- Loans tablosu, `BookId` ve `MemberId` ile bu iki tabloya bağlıdır. Var olmayan bir kitap veya üye için ödünç kaydı eklenemez.

## Kurulum

1. SQL Server Management Studio'yu (SSMS) aç.
2. Bu depodaki `.sql` dosyasını aç.
3. Dosyayı çalıştır. Veritabanı ve tablolar sıfırdan oluşur.

> Not: Betik SQL Server 2025 Express üzerinde oluşturuldu. Veritabanı dosyalarının yolu (CREATE DATABASE kısmı) kendi sunucundaki veri klasörüne göre değiştirilmesi gerekebilir.

## Kullanılan Teknolojiler

- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- C# (planlanıyor)
