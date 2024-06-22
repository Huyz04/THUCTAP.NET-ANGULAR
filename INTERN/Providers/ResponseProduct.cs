using INTERN.DTO;
using INTERN.Model;

namespace INTERN.Providers
{
    public class ResponseProduct
    {
        public IEnumerable<ProductDTO> Collection {get; set;}
        public int Total {get; set;}
        public int PageSize {get; set;}
        public int PageIndex {get; set;}
        public ResponseProduct(IEnumerable<ProductDTO> products, int total = 0, int pagesize = 0, int pageindex = 0)
        {
            this.Collection = products;
            this.Total = total;
            this.PageSize = pagesize;
            this.PageIndex = pageindex;
        }
        public ResponseProduct()
        {
            this.Collection = new List<ProductDTO>();
            this.Total = 0;
            this.PageSize = 0;
            this.PageIndex = 0;
        }
    }
}
