using Microsoft.EntityFrameworkCore;
using NewsRoom.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.GlobalConstants.FaqEntity;


namespace NewsRoom.Data.Models
{
    
    public class FaqEntity : BaseDeletableModel<int>
    {
        // Faq could be only created and updated from admin.
        // User are only able to read.
        [Required]
        [MaxLength(QuestionMaxLength)]
        public string Question { get; set; }

        [Required]
        [MaxLength(AnswerMaxLength)]
        public string Answer { get; set; }
    }
}
