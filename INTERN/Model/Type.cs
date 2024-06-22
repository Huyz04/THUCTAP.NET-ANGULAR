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
        // Navigation property
        public ICollection<Product> products { get; set; }
    }
}   
