using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsRoom.Models.News
{
    public class AddNewsFormModel
    {
     
        public string Area { get; init; }
     
        public string Title { get; init; }

        public string Description { get; init; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        public DateTime Date { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<NewsCategoryViewModel> Categories { get; set; }
    } 
}
