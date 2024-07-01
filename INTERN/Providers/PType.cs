using AutoMapper;
using INTERN.DTO;
using INTERN.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        public async Task<ActionResult<ResponseTypeStatus>> PGetTypes()
        {
            var r = new ResponseTypeStatus();

            try
            {
                if (_context == null || _mapper == null)
                {
                    r.Success = false;
                    r.Message = "Internal server error: Dependencies are not initialized.";
                    return r;
                }

                var types = await _context.Types.ToListAsync();
                IEnumerable<TypeDTO> x = _mapper.Map<IEnumerable<TypeDTO>>(types);

                if (x != null && x.Any())
                {
                    r.Success = true;
                    r.data.Collection = x;
                }
                else
                {
                    r.Success = false;
                    r.Message = "No types found.";
                }
            }
            catch (Exception ex)
            {
                r.Success = false;
                r.Message = $"An error occurred: {ex.Message}";
            }

            return r;
        }
        public async Task<ActionResult<ResponseTypeStatus>> PGetType(int id)
        {
            var r = new ResponseTypeStatus();

            try
            {
                if (_context == null || _mapper == null)
                {
                    r.Success = false;
                    r.Message = "Internal server error: Dependencies are not initialized.";
                    return r;
                }

                var types = await _context.Types.FindAsync(id);
                TypeDTO y = _mapper.Map<TypeDTO>(types);
                IEnumerable<TypeDTO> x = new List<TypeDTO> {y};
                if (x != null && x.Any())
                {
                    r.Success = true;
                    r.data = new ResponseType(x,1,1,1);
                }
                else
                {
                    r.Success = false;
                    r.Message = "No types found.";
                }
            }
            catch (Exception ex)
            {
                r.Success = false;
                r.Message = $"An error occurred: {ex.Message}";
            }

            return r;
        }

        // PUT: api/Types/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<ActionResult<ResponseTypeStatus>> PutType(int id, TypeDTO @type, HttpContext httpContext)
        {
            var r = new ResponseTypeStatus();
            if (id != @type.TypeId)
            {
                r.Success = false;
                r.Message = "Front-end post no match Id";
                return r;
            }
            try
            {
                if (_context == null || _mapper == null)
                {
                    r.Success = false;
                    r.Message = "Internal server error: Dependencies are not initialized.";
                    return r;
                }
                var typee1 = _context.Types.AsNoTracking().FirstOrDefault(p => p.TypeId == id);
                if (typee1 == null)
                {
                    r.Success = false;
                    r.Message = "Can't find this Type";
                    return r;
                }
                var typee = _mapper.Map<Model.Type>(@type);
                typee.Created_at = typee1.Created_at;
                typee.Created_by = typee1.Created_by;
                typee.Updated_at = DateTime.Now;
                typee.Updated_by = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Update(typee);
                await _context.SaveChangesAsync();
                var types = await _context.Types.ToListAsync();
                IEnumerable<TypeDTO> x = _mapper.Map<IEnumerable<TypeDTO>>(types);
                r.Success = true;
                r.data.Collection = x;
            }
            catch (Exception ex)
            {
                r.Success = false;
                r.Message = $"An error occurred: {ex.Message}";
            }

            return r;
        }

        // POST: api/Types
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<ActionResult<ResponseTypeStatus>> PostType(TypeDTO typedto, HttpContext httpcontext)
        {
            var r = new ResponseTypeStatus();
            try
            {
                var type = new Model.Type
                {
                    TypeId = typedto.TypeId,
                    NameType = typedto.NameType,
                    Created_at = DateTime.Now,
                    Created_by = httpcontext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Updated_at = DateTime.Now,
                    Updated_by = httpcontext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Types.Add(type);
                await _context.SaveChangesAsync();
                var v = await FindPage("", 1, 10);
                return v;
            }
            catch (Exception ex)
            {
                r.Success = false;
                r.Message = $"An error occurred: {ex.Message}";
            }
            return r;
        }

        // DELETE: api/Types/5
        public async Task<ActionResult<ResponseTypeStatus>> DeleteType(int id)
        {
            ResponseTypeStatus r = new ResponseTypeStatus();
            try
            {
                var @type = await _context.Types.FindAsync(id);
                if (@type == null)
                {
                    r.Success = false;
                    r.Message = "Have No Type Match with This ID";
                    return r;
                }
                var product = _context.Products.Where(p => p.Type.TypeId == id).FirstOrDefault();
                if (product != null)
                {
                    r.Success = false;
                    r.Message = "Can't Remove Because have Product ForeignKey";
                    return r;
                }
                _context.Types.Remove(@type);
                await _context.SaveChangesAsync();
                var v = await FindPage("", 1, 10);
                return v;
            }
            catch (Exception ex) {
                r.Success = false;
                r.Message = $"An error occurred: {ex.Message}";
                return r;
            }
        }
        // GET: api/Product
        
        public async Task<ActionResult<ResponseTypeStatus>> FindPage(string Filters = "", int Page = 1, int PageSize = 10)
        {
            var response = new ResponseTypeStatus();

            try
            {
                var query = _context.Types.AsQueryable();

                // Áp dụng bộ lọc nếu Filters được cung cấp
                if (!string.IsNullOrEmpty(Filters))
                {
                    string filterValue = Filters; // Lấy giá trị Filters từ model
                    query = query.Where(t => t.NameType.Contains(filterValue)); // Lọc theo tên sản phẩm
                }

                var totalItems = await query.CountAsync();

                var items = await query
                    .Skip((Page - 1) * PageSize)
                    .Take(PageSize)
                    .Select(t => new TypeDTO
                    {
                        TypeId = t.TypeId,
                        NameType = t.NameType
                    })
                    .ToListAsync();

                var responseData = new ResponseType
                {
                    Collection = items,
                    Total = totalItems,
                    PageSize = PageSize,
                    PageIndex = Page
                };

                // Prepare the full response
                response.Success = true; // Assuming there are products returned
                response.data = responseData;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        private bool TypeExists(int id)
        {
            return _context.Types.Any(e => e.TypeId == id);
        }
    }
}
