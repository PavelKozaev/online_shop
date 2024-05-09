using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class Role : IdentityRole
    {        
        public Role(string name) : base(name) { }
        public Role() { }
    }
}
