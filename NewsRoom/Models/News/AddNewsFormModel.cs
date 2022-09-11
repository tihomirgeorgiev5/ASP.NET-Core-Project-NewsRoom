using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.DataConstants;

namespace NewsRoom.Models.News
{
    public class AddNewsFormModel
    {
        [Required]
        [StringLength(NewsAreaMaxLength, MinimumLength = NewsAreaMinLength)]

        public string Area { get; init; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = NewsTitleMinLength,
            ErrorMessage = "Sorry characters allowed for Title are too long")]
        public string Title { get; init; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = NewsDescriptionMinLength,
            ErrorMessage = "The field Description must be a text with a minimum length of {2}.")]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        //[DateValidator("01-01-2000")]
        public DateTime Date { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<NewsCategoryViewModel> Categories { get; set; }
    } 
}
