using System.ComponentModel.DataAnnotations;

namespace LibrarySystem_Labajo.Models
{
    public class Borrower
    {

        [Key]
        public int b_Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string b_fname { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string b_lname { get; set; }
        [Required]
        [Display(Name = "Course")]
        public string b_Course { get; set; }

        [Required]
        [Phone]
        [MaxLength(11)]
        [Display(Name = "Phone number")]
        public string b_PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string b_Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime date_registered { get; set; }
    }
}
