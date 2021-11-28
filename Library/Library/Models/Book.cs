using System;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }

        public override string ToString()
        {
            return $"Book #{Id}: {Title} by {Author}\n";
        }
    }
}
