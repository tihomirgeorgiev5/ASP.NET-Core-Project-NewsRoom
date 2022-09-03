using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.DataConstants;

namespace NewsRoom.Models.News
{
    public class AddNewsFormModel
    {
        [Required]
        [MaxLength(NewsAreaMaxLength)]
        [MinLength(NewsAreaMinLength)]

        public string Area { get; init; }

        [Required]
        [MaxLength(NewsTitleMaxLength)]
        [MinLength(NewsTitleMinLength)]
        public string Title { get; init; }

        [Required]
        [MinLength(NewsDescriptionMinLength)]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        
        public DateTime Date { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<NewsCategoryViewModel> Categories { get; set; }
    } 
}
