using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTERN.Model
{
    public class Type
    {
        [Key]
        public int TypeId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string NameType { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created_at { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Created_by { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Updated_at { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Updated_by { get; set; }

        // Navigation property
        public ICollection<Product> products { get; set; }
    }
}   
