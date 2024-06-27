using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTERN.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string Code {  get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }

        [Column(TypeName = "int")]
        public int Price { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime Created_at { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string Created_by { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Updated_at { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Updated_by { get; set; }

        // Navigation property
        public Type Type { get; set; }
    }
}
