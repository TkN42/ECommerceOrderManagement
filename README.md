# ECommerceOrderManagement

Yazılım Geliştirici Pozisyonu - Teknik Değerlendirme Görevi
Proje Adı: E-Ticaret Sipariş Yönetim Sistemi
Proje Amacı:
Adayların aşağıdaki konulardaki bilgi ve becerilerini ölçmek:
•	Katmanlı mimari (Data Access Layer, Business Logic Layer, Presentation Layer)
•	SQL Server ve veritabanı tasarımı
•	Blazor ile kullanıcı arayüzü geliştirme
•	DevExpress bileşenlerini kullanma
•	Entity Framework Core veya Dapper ile veritabanı işlemleri (Güncel olarak projelerimizde Dapper kullanıyoruz.)
•	Temel CRUD (Create, Read, Update, Delete) işlemleri
•	Kullanıcı girişi ve yetkilendirme

Proje Gereksinimleri
1.	Veritabanı Tasarımı:
o	SQL Server 2014 veya üstü kullanılacak.
o	Veritabanı tabloları aşağıdaki gibi olacak:
	Kullanici (Id, KullaniciAdi, Parola)
	Urun (Id, UrunKodu, UrunAdi, Fiyat)
	Siparis (Id, SiparisTarihi, KullaniciId)
	SiparisDetay (Id, SiparisId, UrunId, Miktar, Fiyat)
o	Tablolar arasında ilişkiler kurulacak ve normalizasyon kurallarına uyulacak.
2.	Katmanlı Mimari:
3.	Kullanıcı Arayüzü (UI):
o	Blazor Server veya Blazor WebAssembly kullanılacak.
o	DevExpress bileşenleri kullanılarak modern ve kullanıcı dostu bir arayüz tasarlanacak. (Güncel olarak projelerimizde DevExpress kullanıyoruz. Alternatif olarak Bootstrap tercih edilebilir. )
o	Kullanıcıların aşağıdaki işlemleri yapabilmesi sağlanacak:
	Kullanıcı kaydı ve girişi
	Parola değiştirme
	Ürün listeleme, ekleme, güncelleme, silme
	Sepete ürün ekleme ve çıkarma
	Sipariş oluşturma ve sipariş numarasını görüntüleme
4.	Fonksiyonel Gereksinimler:
o	Kullanıcı kayıt ve giriş işlemleri
o	Kullanıcı giriş yaptıktan sonra:
	Parola değiştirme
	Ürün yönetimi (listeleme, ekleme, güncelleme, silme)
	Sepet işlemleri (ürün ekleme, çıkarma, sipariş oluşturma)
o	Sipariş oluşturulduğunda sipariş numarasının kullanıcıya gösterilmesi

