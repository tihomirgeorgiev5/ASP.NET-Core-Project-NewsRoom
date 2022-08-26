using System.Collections.Generic;

namespace NewsRoom.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ANews> News { get; init; } = new List<ANews>();
    }
}
