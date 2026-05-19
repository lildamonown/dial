using Microsoft.AspNetCore.Identity;

namespace Kursa4.DAL.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public List<Review> Reviews { get; set; } = [];

        public List<Order> Orders { get; set; } = [];
    }
}
