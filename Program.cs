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
        static void KitapSil()
        {
            Console.Write("Silmek istediğiniz kitabın ID'sini girin: ");
            int bookId;
            while (!int.TryParse(Console.ReadLine(), out bookId))
            {
                Console.WriteLine("Geçersiz giriş. Lütfen bir sayı girin.");
                Console.Write("Kitap ID: ");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Books WHERE BookId = @BookId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookId", bookId);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Kitap başarıyla silindi.");
                    }
                    else
                    {
                        Console.WriteLine("Belirtilen ID'ye sahip kitap bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Kitap silme hatası: " + ex.Message);
                }
            }
        }
        static void KitapGuncelle()
        {
            Console.Write("Güncellemek istediğiniz kitabın ID'sini girin: ");
            int bookId;
            while (!int.TryParse(Console.ReadLine(), out bookId))
            {
                Console.WriteLine("Geçersiz giriş. Lütfen bir sayı girin.");
                Console.Write("Kitap ID: ");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Books WHERE BookId = @BookId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookId", bookId);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string currentTitle = reader["Title"].ToString();
                            string currentAuthor = reader["Author"].ToString();
                            string currentCategory = reader["Category"].ToString();
                            int currentPublicationYear = Convert.ToInt32(reader["PublicationYear"]);
                            bool currentIsAvailable = Convert.ToBoolean(reader["IsAvailable"]);
                            Console.WriteLine($"Mevcut Başlık: {currentTitle}");
                            Console.Write("Yeni Başlık (boş bırakılırsa değişmeyecek): ");
                            string newTitle = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newTitle)) newTitle = currentTitle;
                            Console.WriteLine($"Mevcut Yazar: {currentAuthor}");
                            Console.Write("Yeni Yazar (boş bırakılırsa değişmeyecek): ");
                            string newAuthor = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newAuthor)) newAuthor = currentAuthor;
                            Console.WriteLine($"Mevcut Kategori: {currentCategory}");
                            Console.Write("Yeni Kategori (boş bırakılırsa değişmeyecek): ");
                            string newCategory = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newCategory)) newCategory = currentCategory;
                            Console.WriteLine($"Mevcut Yayın Yılı: {currentPublicationYear}");
                            Console.Write("Yeni Yayın Yılı (boş bırakılırsa değişmeyecek): ");
                            string publicationYearInput = Console.ReadLine();
                            int newPublicationYear;
                            if (string.IsNullOrWhiteSpace(publicationYearInput))
                                newPublicationYear = currentPublicationYear;
                            else if (!int.TryParse(publicationYearInput, out newPublicationYear))
                            {
                                Console.WriteLine("Geçersiz yayın yılı. Lütfen bir sayı girin.");
                                newPublicationYear = currentPublicationYear;
                            }
                            Console.WriteLine($"Mevcut Mevcutluk: {currentIsAvailable}");
                            Console.Write("Yeni Mevcutluk (true/false, boş bırakılırsa değişmeyecek): ");
                            string isAvailableInput = Console.ReadLine();
                            bool newIsAvailable;
                            if (string.IsNullOrWhiteSpace(isAvailableInput))
                                newIsAvailable = currentIsAvailable;
                            else if (!bool.TryParse(isAvailableInput, out newIsAvailable))
                            {
                                Console.WriteLine("Geçersiz mevcutluk değeri. Lütfen true veya false girin.");
                                newIsAvailable = currentIsAvailable;
                            }

                            string updateQuery = "UPDATE Books SET Title = @Title, Author = @Author, Category = @Category, PublicationYear = @PublicationYear, IsAvailable = @IsAvailable WHERE BookId = @BookId";
                            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                            updateCommand.Parameters.AddWithValue("@Title", newTitle);
                            updateCommand.Parameters.AddWithValue("@Author", newAuthor);
                            updateCommand.Parameters.AddWithValue("@Category", newCategory);
                            updateCommand.Parameters.AddWithValue("@PublicationYear", newPublicationYear);
                            updateCommand.Parameters.AddWithValue("@IsAvailable", newIsAvailable);
                            updateCommand.Parameters.AddWithValue("@BookId", bookId);

                            try
                            {
                                updateCommand.ExecuteNonQuery();
                                Console.WriteLine("Kitap başarıyla güncellendi.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Hata oluştu: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Belirtilen ID ile kitap bulunamadı.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata oluştu: {ex.Message}");
                }
            }
        }
        static void KitapAra()
        {
            Console.Write("Aramak istediğiniz kitabın başlığını girin: ");
            string title = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Books WHERE Title LIKE @Title";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", "%" + title + "%");
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Arama Sonuçları:");
                        while (reader.Read())
                        {
                            string durum = reader["IsAvailable"].ToString() == "True" ? "Durum: Rafta" : "Durum: Ödünçte";
                            Console.WriteLine($"ID: {reader["BookId"]}, Başlık: {reader["Title"]}, Yazar: {reader["Author"]}, Kategori: {reader["Category"]}, Yayın Yılı: {reader["PublicationYear"]}, Mevcut: {durum}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Kitap arama hatası: " + ex.Message);
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
                Console.WriteLine("5. Kitap Ara");
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
                    KitapSil();
                }
                else if (secim == 3)
                {
                    KitapGuncelle();
                }
                else if (secim == 4)
                {
                    KitaplariListele();
                }
                else if (secim == 5)
                {
                    KitapAra();
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

