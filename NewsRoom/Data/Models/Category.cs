using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.GlobalConstants.Category;

namespace NewsRoom.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<News> News { get; init; } = new List<News>();
    }
}
