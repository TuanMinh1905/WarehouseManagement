using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

public class Carts
{
    [Key]
    public int CartID { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public int ProductID { get; set; }

    public virtual Products Product { get; set; }
}
