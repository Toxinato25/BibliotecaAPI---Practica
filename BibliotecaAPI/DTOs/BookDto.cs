namespace BibliotecaAPI.DTOs
{
    public class BookDto
    {
        public int BookId { get; set;  } // solo lectura
        public string Title { get; set; }

        public string Author { get; set; }

        public int PublishedYear { get; set; }

        public string Genre { get; set; }
    }
}
