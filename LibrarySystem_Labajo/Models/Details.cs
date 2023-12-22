using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem_Labajo.Models
{
    public class Details
    {
        [Key]
        public int details_id { get; set; }

  
        [Display(Name = "For Books")]
        public int? books_id { get; set; }

        [ForeignKey("books_id")]
        public Books? FK_books_id { get; set; }


        [Display(Name = "For Records")]
        public int? record_id { get; set; }

        [ForeignKey("record_id")]
        [Display(Name = "Record_Id")]
        public Records? FK_record_id { get; set; }


        //hide this in create
        //this is used for edit
        //in details, craete a logic that will show all the the data that return date 

        [DataType(DataType.DateTime)]
        [Display(Name = "Date Return")]
        public DateTime? return_date { get; set; }
    }
}
