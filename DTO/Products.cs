using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Products
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    [StringLength(50)]
    public string ProductName { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public int Unit { get; set; }

    // Foreign Key
    public int CategoryID { get; set; }


    [ForeignKey("CategoryID")]
    public virtual Categories Category { get; set; }
    public virtual ICollection<Carts> Carts { get; set; }
    public virtual ICollection<Histories> Histories { get; set; }
}
