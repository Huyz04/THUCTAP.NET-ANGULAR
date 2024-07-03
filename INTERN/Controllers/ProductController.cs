using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INTERN.Model;
using INTERN.Providers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using INTERN.DTO;
using AutoMapper;
using NuGet.Versioning;
using Microsoft.AspNetCore.Authorization;
using static INTERN.Helper.ServiceResponse;


namespace INTERN.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;
        private readonly PProduct _pProduct;
        public ProductController(ProductContext context, PProduct pProduct,IMapper mapper)
        {
            _context = context;
            _pProduct = pProduct;
            _mapper = mapper;   
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public Task<ActionResult<Response>> GetProduct(int id)
        {
            return _pProduct.GetProductId(id);
        }
        // GET: api/Product
        [HttpGet("FindPage")]
        public Task<ActionResult<Response>> GetProducts(
            [FromQuery] string Filters = null,
            [FromQuery] string Feature = null,
            [FromQuery] int Page = 1,
            [FromQuery] int PageSize = 10)
        {
            return _pProduct.GetProductFindPage(Filters, Feature, Page, PageSize);
        }


        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public Task<ActionResult<Response>> PutProduct(int id,[FromBody] ProductDTO productdto)
        {
            return _pProduct.PutProduct(id, productdto, HttpContext);
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Task<ActionResult<Response>> PostProduct([FromBody] ProductDTO product)
        {
            return _pProduct.PostProduct(product, HttpContext);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public Task<ActionResult<Response>> DeleteProduct(int id)
        {
            return _pProduct.DeleteProduct(id);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
