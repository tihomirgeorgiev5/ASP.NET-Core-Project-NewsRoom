using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.GlobalConstants.FaqEntity;
using static NewsRoom.Data.MessageConstants.AllError;

namespace NewsRoom.Areas.Admin.Models
{
    public class FaqCreateInputViewModel
    {
        [Required(ErrorMessage = EmptyField)]
        [MinLength(QuestionMinLength)]
        [MaxLength(QuestionMaxLength)]
        [BindProperty]

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
