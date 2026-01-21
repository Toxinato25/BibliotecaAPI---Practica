namespace BibliotecaAPI.DTOs
{
    public class LoanDto
    {
        public int LoanId { get; set; } // solo lectura

        public int UserId { get; set; }

        public string UserName { get; set; } // de Users.username

        public int BookId { get; set; }

        public string BookTitle { get; set; } // de Books.Title

        public DateTime LoanDate { get; set; } = DateTime.Now;


        public DateTime? ReturnDate { get; set; }

       
       
    }
}
