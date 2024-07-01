using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTERN.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? UserName {  get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Password { get; set; }
    }
}
