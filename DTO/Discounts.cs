using System.ComponentModel.DataAnnotations;

public class Discounts
{
    [Key]
    public int DiscountID { get; set; }
    public decimal Value { get; set; }
    public string Description { get; set; }
    public override string ToString()
    {
        return Description;
    }
}
