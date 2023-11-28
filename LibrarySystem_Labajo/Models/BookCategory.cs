using System.ComponentModel.DataAnnotations;

namespace LibrarySystem_Labajo.Models
{
    public class BookCategory
    {
        //property
        [Key]
        [Display(Name = "Category ID")]
        public int categoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string categoryName { get; set; }
    }
}
