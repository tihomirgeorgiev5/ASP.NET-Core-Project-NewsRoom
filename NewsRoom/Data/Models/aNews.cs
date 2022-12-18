using System;
using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.DataConstants.ANews;

namespace NewsRoom.Data.Models
{
    public class ANews
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(AreaMaxLength)]
        public string Area { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }

        public bool IsPublic { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public int JournalistId { get; init; }

        public Journalist Journalist { get; init; }


    }
}
