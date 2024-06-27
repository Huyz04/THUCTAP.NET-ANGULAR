using Microsoft.AspNetCore.Identity;

namespace INTERN.Model
{
    public class User : IdentityUser
    {
        public string? Name {  get; set; }
    }
}
