using System.ComponentModel.DataAnnotations;

namespace INTERN.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
