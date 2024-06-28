using System.ComponentModel.DataAnnotations.Schema;

namespace INTERN.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string TypeName { get; set; }
    }
}
