using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneDb
{
    internal class Program
    {
        static string connectionString = "Data Source=.\\SQLEXPRESS2025;Initial Catalog=KutuphaneDb;Integrated Security=True";

        static void BaglantiTest()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Veritabanına bağlantı başarılı!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Veritabanına bağlantı başarısız: " + ex.Message);
                }
            }
        }
        static void KitaplariListele()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Books";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("Kitap Listesi:");
                    while (reader.Read())
                    {
                        string durum = reader["IsAvailable"].ToString() == "True" ? "Mevcut" : "Mevcut Değil";
                        Console.WriteLine($"ID: {reader["BookId"]}, Başlık: {reader["Title"]}, Yazar: {reader["Author"]}, Kategori: {reader["Category"]}, Yayın Yılı: {reader["PublicationYear"]}, Mevcut: {durum}");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Kitapları listeleme hatası: " + ex.Message);
                }
            }
        }

        static void Main(string[] args)
        {
            BaglantiTest();
            int secim;
            do
            {
                Console.WriteLine("0. Çıkış");
                Console.WriteLine("1. Kitap Ekle");
                Console.WriteLine("2. Kitap Sil");
                Console.WriteLine("3. Kitap Güncelle");
                Console.WriteLine("4. Kitapları Listele");
                Console.Write("Lütfen yapmak istediğiniz işlemi seçin:");
                secim = Convert.ToInt32(Console.ReadLine());
                if(secim == 1)
                {
                    // Kitap ekleme işlemi
                }
                else if (secim == 2)
                {
                    // Kitap silme işlemi
                }
                else if (secim == 3)
                {
                    // Kitap güncelleme işlemi
                }
                else if (secim == 4)
                {
                    KitaplariListele();
                }
                else if (secim != 0)
                {
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                }


            } while(secim != 0);

            Console.ReadLine();
        }
        
    }
}

