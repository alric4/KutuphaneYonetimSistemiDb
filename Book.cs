using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneDb
{

    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }
        public Book(string title, string author, string category, int publicationYear, bool isAvailable)
        {
            Title = title;
            Author = author;
            Category = category;
            PublicationYear = publicationYear;
            IsAvailable = isAvailable;
        }

    }
}
