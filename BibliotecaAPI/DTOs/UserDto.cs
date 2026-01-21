namespace BibliotecaAPI.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; } // solo lectura
        public string UserName { get; set; }
        public string Email { get; set; }

        public DateTime? RegisteredAt { get; set; } = DateTime.Now;
        // formato string "yyyy-MM-dd"

    }
}
