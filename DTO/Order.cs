using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    public int OrderID { get; set; }
    public string TransactionId { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }
    public int Quantity { get; set; }

}
