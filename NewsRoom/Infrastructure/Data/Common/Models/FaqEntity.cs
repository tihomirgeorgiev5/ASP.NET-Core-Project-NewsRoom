﻿using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.GlobalConstants.FaqEntity;

namespace NewsRoom.Infrastructure.Data.Common.Models
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
