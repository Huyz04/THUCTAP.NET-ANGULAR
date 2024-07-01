using AutoMapper;
using INTERN.DTO;
using INTERN.Helper;
using INTERN.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static NuGet.Packaging.PackagingConstants;
using System.Drawing.Printing;
using System.Security.Claims;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;

namespace INTERN.Providers
{
    public class PProduct
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;
        public PProduct(ProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActionResult<Response>> PGetProducts()
        {
            Response response = new Response();
            IEnumerable<ProductDTO> listProduct = _mapper.Map<IEnumerable<ProductDTO>>(await _context.Products.ToListAsync());
            if(listProduct != null) response.data.Collection = listProduct;
            else response.data.Collection = new List<ProductDTO>();
            response.data.Total = await _context.Products.CountAsync();
            response.data.PageSize = 0;
            response.data.PageIndex = 0;
            if (response.data.Collection != null) response.Success = true;
            else response.Success = false;
            return response;
        }
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return null; 
            }
            return product;
        }
        public async Task<ActionResult<Response>> GetProductFindPage(string filter,string feature, int page, int pagesize)
        {

            var query = _context.Products.AsQueryable();
            query = query.Include(p => p.Type);

            // Áp dụng bộ lọc nếu Filters được cung cấp
            if (!string.IsNullOrEmpty(filter))
            {
                if (feature == "Name")
                {
                    query = query.Where(p => p.Name.Contains(filter)); // Lọc theo tên sản phẩm
                }
                else if (feature == "Type")
                {
                    query = query.Where(p => p.Type.NameType.Contains(filter)); // Lọc theo tên sản phẩm
                }
            }

            var totalItems = await query.CountAsync();

            var products = await query
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

            var response = new ResponseProduct
            {
                Collection = _mapper.Map<IEnumerable<ProductDTO>>(products),
                Total = totalItems,
                PageSize = pagesize,
                PageIndex = page
            };

            // Prepare the full response
            var fullResponse = new Response
            {
                Success = true, // Assuming there are products returned
                data = response
            };

            return fullResponse;
        }
        public async Task<ActionResult<Response>> GetProductId(int id)
        {
            var product = _mapper.Map<ProductDTO>(await _context.Products.Include(p => p.Type).FirstOrDefaultAsync(p => p.Id == id));
            Response R = new Response();
            if (product == null)
            {
                R.Success = false;
                R.data.Total = 0;
                R.Message = "Have No Product Match with This ID!";
                return R;
            }
            IEnumerable<ProductDTO> a = new List<ProductDTO>() { product };
            R.Success = true;
            R.data.Collection = a;
            R.data.Total = 1;
            R.data.PageIndex = 1;
            return R;
        }
        public async Task<ActionResult<Response>> PostProduct([FromBody] ProductDTO product, HttpContext httpContext)
        {

            Response r = new Response();
            if (product.Id != 0)
            {
                r.Success = false;
                r.Message = "Please do not fill in the ID field!";
                return r;
            }
            try
            {
                
                Product prd = new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Code = product.Code,
                    Description = product.Description,
                    Price = product.Price,
                    Created_at = DateTime.Now,
                    Created_by = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Updated_at = DateTime.Now,
                    Updated_by = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                prd.Type = _context.Types.Where(t => t.NameType == product.TypeName).FirstOrDefault();
                _context.Products.Add(prd);
                await _context.SaveChangesAsync();
                var re = await GetProductFindPage(null, null, 1, 10);
                return re;
            }
            catch (Exception ex) 
            {
                r.Success = false;
                r.Message = "Have No Type Match with This TypeName";
                return r;
            }
        }
        public async Task<ActionResult<Response>> DeleteProduct(int id)
        {
            Response r = new Response();
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    r.Success = false;
                    r.Message = "Have No Product Match With This ProductID!";
                    return r;
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                var re = await GetProductFindPage(null, null, 1, 10);
                return re;
            }
            catch (Exception ex)
            {
                r.Success = false;
                r.Message = $"An error occurred: {ex.Message}";
                return r;
            }

        }
        public async Task<ActionResult<Response>> PutProduct(int IdProduct,[FromBody] ProductDTO productdto, HttpContext httpContext)
        {
            Response r = new Response();
            if (IdProduct != productdto.Id)
            {
                r.Success = false; 
                r.Message = "The ID Not Match!";

                return r;
            }
            var product = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == IdProduct);

            try
            { 
                Product prd = new Product()
                {
                    Id = productdto.Id,
                    Name = productdto.Name,
                    Code = productdto.Code,
                    Description = productdto.Description,
                    Price = productdto.Price,
                    Created_at = product.Created_at,
                    Created_by = product.Created_by,
                    Updated_at = DateTime.Now,
                    Updated_by = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                prd.Type = _context.Types.Where(t => t.NameType == productdto.TypeName).FirstOrDefault();

                _context.Entry(prd).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                var result = await GetProductId(IdProduct);
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductExists(IdProduct))
                {
                    r.Success = false;
                    r.Message = $"An error occurred: {ex.Message}";
                    return r;
                }
                else
                {
                    throw;
                }
            }

        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }

}
