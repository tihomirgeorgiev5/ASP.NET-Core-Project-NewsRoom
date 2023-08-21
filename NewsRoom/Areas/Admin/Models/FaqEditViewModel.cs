using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.MessageConstants.AllError;
using static NewsRoom.Data.GlobalConstants.FaqEntity;

namespace NewsRoom.Areas.Admin.Models
{
    public class FaqEditViewModel
    {
        public int FaqId { get; set; }

        [Required(ErrorMessage = EmptyField)]
        [MaxLength(QuestionMaxLength)]
        [MinLength(QuestionMinLength)]

        public string Question { get; set; }

        [Required(ErrorMessage =EmptyField)]
        [MaxLength(AnswerMaxLength)]
        [MinLength(AnswerMinLength)]
        public string Answer { get; set; }
    }
}
