using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.DataConstants.User;

namespace NewsRoom.Data.Models
{
    public class User : IdentityUser 
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }
    }
}
