using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem_Labajo.Models
{
    public class Records
    {
        [Key]
        public int record_id { get; set; }

        [Required]
        [Display(Name = "Borrower ID & Name")]
        public int borrowerId { get; set; }


        [ForeignKey("borrowerId")]
        [Display(Name = "Full Name")]
        public Borrower? FK_borrower { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime due_date { get; set; }

        [Required]
        [Display(Name = "Libratian Name")]
        public int librarianId { get; set; }

        [ForeignKey("librarianId")]
        public User? FK_librarian { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? transac_date { get; set; }

    }

}
