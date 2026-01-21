using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Data
{
    public class Loans
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Books Book { get; set; }

        [Column(TypeName = "date")]
        public DateTime LoanDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReturnDate { get; set; }
    }
}
