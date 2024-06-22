using INTERN.Model;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace INTERN.Providers
{
    public class ResponseType
    {
        public IEnumerable<DTO.TypeDTO> Collection { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public ResponseType(IEnumerable<DTO.TypeDTO> type, int total = 0, int pagesize = 0, int pageindex = 0)
        {
            this.Collection = type;
            this.Total = total;
            this.PageSize = pagesize;
            this.PageIndex = pageindex;
        }
        public ResponseType()
        {
            this.Collection = new List<DTO.TypeDTO>();
            this.Total = 0;
            this.PageSize = 0;
            this.PageIndex = 0;
        }
    }
}
