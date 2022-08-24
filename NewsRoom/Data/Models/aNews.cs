using System;
using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.DataConstants;

namespace NewsRoom.Data.Models
{
    public class ANews
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NewsAreaMaxLength)]
        public string Area { get; set; }

        [Required]
        [MaxLength(NewsTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(NewsDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }


    }
}
