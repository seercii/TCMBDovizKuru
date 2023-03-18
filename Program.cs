using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MerkezBankası
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) //burada bir döngü oluşturdum
            {
                try  //burada hata durumlarına göre kullandım 
                {
                    WebClient client = new WebClient(); // Web isteklerini yapmak için WebClient sınıfını kullandım
                    string xmlData = client.DownloadString("https://www.tcmb.gov.tr/kurlar/today.xml");
                    XDocument doc = XDocument.Parse(xmlData); // XML dosyası XElement sınıfına dönüştürdüm.

                    // XElement sınıfı kullanarak TCMB Kur Listesi'nden USD ve EUR kurunu alıyoruz
                    string usdKur = doc.Element("Tarih_Date").Element("Currency").Element("ForexSelling").Value;
                    string eurKur = doc.Element("Tarih_Date").Element("Currency").Element("ForexBuying").Value;

                    SqlConnection connection = new SqlConnection("Server=91.93.1.160;Database=sergu;User Id=sergu;Password=SercanGure*1879;");
                    connection.Open();// Veritabanı bağlantısı oluşturuluyor ve bağlantı açılıyor
                    SqlCommand command = new SqlCommand("INSERT INTO TCMB (Kur, Deger, Tarih) VALUES (@kur, @deger, @tarih)", connection); // Veritabanına veri eklemek için SqlCommand sınıfı kullandım
                    command.Parameters.AddWithValue("@kur", "USD"); // Veri ekleme sorgusunda kullanılacak parametreleri belirttim
                    command.Parameters.AddWithValue("@deger", float.Parse(usdKur.Replace('.', ',')));
                    command.Parameters.AddWithValue("@tarih", DateTime.Now);
                    command.ExecuteNonQuery();// Veri ekleme sorgusunu çalıstırıyorum
                    command.Parameters["@kur"].Value = "EUR";  // Parametreleri yeniden kullanılıyor
                    command.Parameters["@deger"].Value = float.Parse(eurKur.Replace('.', ','));
                    command.ExecuteNonQuery();
                    connection.Close(); // Bağlantı kapatılıyor

                    Console.WriteLine("Kur bilgileri başarıyla güncellendi."); // İşlem başarılıysa konsola bir mesaj yazdırılıyor
                }
                catch (Exception ex)// Hata durumunda catch bloğu çalışıyor
                {
                    Console.WriteLine("Hata oluştu: " + ex.Message);// Hata mesajı konsola yazdırılıyor
                }

                Thread.Sleep(60000); // Servisin her dakika çalışması için 1 dakika bekleme süresi veriliyor
            }
        }
    }
}
//İlk olarak MS SQL'de tablo oluşturdum.Ondan sonra .NET FrameWork konsol uygulaması açtım. Ondan sonra
//System.Xml.Linq  ve System.Data.SqlClient kütüphanelerini yükledim. 
//Sonra sonsuz bir döngü başlattım bu döngü sürekli döndüğü için kur verileri veri tabanına kayıt ediliyor.
//Verileri TCMB den almak içim webclient sınıfını kullandım downloadstring metodu ile xml dosyasını indirim.
//xdocument sınıfını kullandım ve bunu xml dosyasını xelement sınfınına dönüştürdüm.
//xelement sınıfı sayesınde usd ve eur kur verilerini aldım
//veri tabanı bağlantısı için sqlconnection sınıfını kullandım vermiş oldugunuz bilgileri girdim.
//veri ekleme içim sqlcommand sınıfını kullandım parameter.addwithvalue metodu ile parametreleri belirttim. kur,deger ve tarih
//executenonquery metodu ile veri ekleme sorgusunu çalıstırdım.
//sonrasında bağlantı kapanıyo ve işlem başarılıysa başarılı mesajı yazdırıyo değilse hata mesajı yazdırılıyo
//servisin her dakika çalışması için thread.sleeep kullandım 1 dakika içim 60000 yazdım.
//kod içinde veritabanına ekleme sorgusu "INSERT INTO TCMB (Kur, Deger, Tarih) VALUES (@kur, @deger, @tarih)" 
//bu sorgu TCMB tablosuna verileri ekler
//command.parameterwithvalue metodu sorgudaki parametrenin değerini belirtiyo mesela kur = usd gibi kur parametresine usd değerini atıyo
// float.parse metodu usdkur ve eurkur değişkenleri ondalık sayı formatında yazar
