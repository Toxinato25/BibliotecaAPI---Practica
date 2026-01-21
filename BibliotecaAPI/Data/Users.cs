using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Data
{
    [Index(nameof(Email), IsUnique = true)] // hace un indece que es el email unico
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime RegisteredAt { get; set; }
    }
}
