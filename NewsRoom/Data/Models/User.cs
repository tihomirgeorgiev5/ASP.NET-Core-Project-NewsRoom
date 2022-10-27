using Microsoft.AspNetCore.Identity;

namespace NewsRoom.Data.Models
{
    public class User : IdentityUser 
    {
        public string FullName { get; set; }
    }
}
