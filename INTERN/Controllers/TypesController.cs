using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INTERN.Model;
using Type = INTERN.Model.Type;
using INTERN.Providers;
using AutoMapper;
using INTERN.DTO;

namespace INTERN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;

        public TypesController(ProductContext context, IMapper mapper)
        {
            _context = context;
           _mapper = mapper;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetTypes()
        {
            IEnumerable<TypeDTO> x= _mapper.Map<IEnumerable<TypeDTO>>(await _context.Types.ToListAsync());
            return Ok(x);
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Type>> GetType(int id)
        {
            var typee = await _context.Types.FindAsync(id);
            if (typee == null)
            {
                return null;
            }
            return typee;
        }

        // PUT: api/Types/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseTypeStatus>> PutType(int id, TypeDTO @type)
        {
            ResponseTypeStatus status = new ResponseTypeStatus();
            if (id != @type.TypeId)
            {
                status.Success = false;
                return status;
            }
            var typee = _mapper.Map<Type>(@type);
            _context.Update(typee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
                {
                    status.Success = false;
                    return status;
                }
                else
                {
                    throw;
                }
            }
            status.Success = true;
            return status;
        }

        // POST: api/Types
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeDTO>> PostType(TypeDTO typedto)
        {

            if (_context.Types == null)
            {
                return Problem("Entity set 'ProductContext.Types' is NULL");
            }
            var type = new Type
            {
                TypeId = typedto.TypeId,
                NameType = typedto.NameType
            };
            _context.Types.Add(type);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetType), new { id = type.TypeId }, type);
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteType(int id)
        {
            Response r = new Response();
            var @type = await _context.Types.FindAsync(id);
            if (@type == null)
            {
                r.Success = false;
                return r;
            }
            var product = _context.Products.Where(p => p.Type.TypeId == id).FirstOrDefault();
            if (product != null)
            {
                r.Success = false;
                return r;
            }
            _context.Types.Remove(@type);
            await _context.SaveChangesAsync();
            r.Success = true;
            return r;
        }
        // GET: api/Product
        [HttpGet("FindPage")]
        public async Task<ActionResult<ResponseTypeStatus>> GetProducts(
            [FromQuery] string Filters = null,
            [FromQuery] int Page = 1,
            [FromQuery] int PageSize = 10)
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

            var response = new ResponseType
            {
                Collection = items,
                Total = totalItems,
                PageSize = PageSize,
                PageIndex = Page
            };

            // Prepare the full response
            var fullResponse = new ResponseTypeStatus
            {
                Success = true, // Assuming there are products returned
                data = response
            };

            return Ok(fullResponse);
        }
        private bool TypeExists(int id)
        {
            return _context.Types.Any(e => e.TypeId == id);
        }
    }
}
