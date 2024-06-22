using AutoMapper;
using INTERN.Model;

namespace INTERN.Providers
{
    public class PType
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;
        public PType(ProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}
