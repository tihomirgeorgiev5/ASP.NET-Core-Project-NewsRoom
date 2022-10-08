using System.ComponentModel.DataAnnotations;
using static NewsRoom.Data.DataConstants.Journalist;


namespace NewsRoom.Models.Journalists
{
    public class BecomeJournalistFormModel
    {

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
