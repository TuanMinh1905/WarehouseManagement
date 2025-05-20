using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Categories
{
    [Key]
    public int CategoryID { get; set; }

    [Required]
    [StringLength(50)]
    public string CategoryItem { get; set; }

    // Navigation Properties
    public virtual ICollection<Products> Products { get; set; }
}
