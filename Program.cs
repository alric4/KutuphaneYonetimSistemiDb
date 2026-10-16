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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Kitap Listesi:");
                        while (reader.Read())
                        {
                            string durum = reader["IsAvailable"].ToString() == "True" ? "Durum: Rafta" : "Durum: Ödünçte";
                            Console.WriteLine($"ID: {reader["BookId"]}, Başlık: {reader["Title"]}, Yazar: {reader["Author"]}, Kategori: {reader["Category"]}, Yayın Yılı: {reader["PublicationYear"]}, Mevcut: {durum}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Kitapları listeleme hatası: " + ex.Message);
                }
            }
        }
        static void KitapEkle()
        {

            Console.Write("Kitap Başlığı: ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Kitap başlığı boş olamaz. Lütfen tekrar deneyin.");
                return;
            }
            Console.Write("Yazar: ");
            string author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Yazar adı boş olamaz. Lütfen tekrar deneyin.");
                return;
            }
            Console.Write("Kategori: ");
            string category = Console.ReadLine();
            Console.Write("Yayın Yılı: ");
            int publicationYear;
            while (!int.TryParse(Console.ReadLine(), out publicationYear))
            {
                Console.WriteLine("Geçersiz giriş. Lütfen bir sayı girin.");
                Console.Write("Yayın Yılı: ");
            }
            bool isAvailable = true;
            Book kitap = new Book(title, author, category, publicationYear, isAvailable);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Books (Title, Author, Category, PublicationYear, IsAvailable) VALUES (@Title, @Author, @Category, @PublicationYear, @IsAvailable)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", kitap.Title);
                command.Parameters.AddWithValue("@Author", kitap.Author);
                command.Parameters.AddWithValue("@Category", kitap.Category);
                command.Parameters.AddWithValue("@PublicationYear", kitap.PublicationYear);
                command.Parameters.AddWithValue("@IsAvailable", kitap.IsAvailable);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Kitap başarıyla eklendi.");
                    }
                    else
                    {
                        Console.WriteLine("Kitap eklenirken bir hata oluştu.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Kitap ekleme hatası: " + ex.Message);
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
                string secimStr = Console.ReadLine();
                if (!int.TryParse(secimStr, out secim))
                {
                    Console.WriteLine("Geçersiz giriş. Lütfen bir sayı girin.");
                    secim = -1;
                    continue;
                }
                if (secim == 1)
                {
                    KitapEkle();
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


            } while (secim != 0);

            Console.ReadLine();
        }

    }
}

