using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace INTERN.Model
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options):base(options) 
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}
