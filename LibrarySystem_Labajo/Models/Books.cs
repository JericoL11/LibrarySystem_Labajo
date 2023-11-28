using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem_Labajo.Models
{
    public class Books
    {
        //property
        [Key]
        public int bookId { get; set; }
        [Required]

        [Display(Name = "Book Name")]
        public string? bookName { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string? author { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public int categoryId { get; set; }

        [ForeignKey("categoryId")]
        [Display(Name = "Category ID")]
        public BookCategory? bookcategory { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime dateAdded { get; set; }


    }
}
