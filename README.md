# MerkezBankası

İlk olarak MS SQL'de tablo oluşturdum.Ondan sonra .NET FrameWork konsol uygulaması açtım. Ondan sonra
System.Xml.Linq  ve System.Data.SqlClient kütüphanelerini yükledim. 
Sonra sonsuz bir döngü başlattım bu döngü sürekli döndüğü için kur verileri veri tabanına kayıt ediliyor.
Verileri TCMB den almak içim webclient sınıfını kullandım downloadstring metodu ile xml dosyasını indirim.
xdocument sınıfını kullandım ve bunu xml dosyasını xelement sınfınına dönüştürdüm.
xelement sınıfı sayesınde usd ve eur kur verilerini aldım
veri tabanı bağlantısı için sqlconnection sınıfını kullandım vermiş oldugunuz bilgileri girdim.
veri ekleme içim sqlcommand sınıfını kullandım parameter.addwithvalue metodu ile parametreleri belirttim. kur,deger ve tarih
executenonquery metodu ile veri ekleme sorgusunu çalıstırdım.
sonrasında bağlantı kapanıyo ve işlem başarılıysa başarılı mesajı yazdırıyo değilse hata mesajı yazdırılıyo
servisin her dakika çalışması için thread.sleeep kullandım 1 dakika içim 60000 yazdım.
kod içinde veritabanına ekleme sorgusu "INSERT INTO TCMB (Kur, Deger, Tarih) VALUES (@kur, @deger, @tarih)" 
bu sorgu TCMB tablosuna verileri ekler
command.parameterwithvalue metodu sorgudaki parametrenin değerini belirtiyo mesela kur = usd gibi kur parametresine usd değerini atıyo
 float.parse metodu usdkur ve eurkur değişkenleri ondalık sayı formatında yazar
