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
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.CodeAnalysis;

namespace INTERN.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;
        private readonly PType _pType;

        public TypesController(ProductContext context, IMapper mapper, PType pType)
        {
            _context = context;
           _mapper = mapper;
            _pType = pType;
        }

        // GET: api/Types
        [HttpGet]
        public Task<ActionResult<ResponseTypeStatus>> GetProducts(
            [FromQuery] string Filters = null,
            [FromQuery] int Page = 1,
            [FromQuery] int PageSize = 10)
        {
            return _pType.FindPage(Filters, Page, PageSize);
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public Task<ActionResult<ResponseTypeStatus>> GetType(int id)
        {
            return _pType.PGetType(id);
        }

        // PUT: api/Types/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public Task<ActionResult<ResponseTypeStatus>> PutType(int id,[FromBody] TypeDTO type)
        {
            return _pType.PutType(id, type, HttpContext);
        }

        // POST: api/Types
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Task<ActionResult<ResponseTypeStatus>> PostType([FromBody] TypeDTO typedto)
        {
            return _pType.PostType(typedto, HttpContext);
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public Task<ActionResult<ResponseTypeStatus>> DeleteType(int id)
        {
            return _pType.DeleteType(id);
        }
    }
}
