
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.GlobalConstants.Journalist;

namespace NewsRoom.Data.Models
{
    public class Journalist
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<News> News { get; init; } = new List<News>();


    }
}
