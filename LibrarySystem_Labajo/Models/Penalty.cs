using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem_Labajo.Models
{
    public class Penalty
    {
        [Key]
        public int? Penalty_Id { get; set; }
        public double? Amount { get; set; }

        [Display(Name = "Penalty Date")]
        [DataType(DataType.Date)]
        public DateTime? Penalty_date { get; set; }

        public bool? IsSettled { get; set; }

        //Prototype OF FK_details

        [Display(Name = "for details_Id")]
        public int? P_details_Id { get; set; }

        [ForeignKey("details_id")]
        public Details? FK_details { get; set; }

        public string? Penalty_name { get; set; }
    }
}
