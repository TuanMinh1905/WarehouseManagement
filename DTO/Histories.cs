using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Histories
{
    [Key]
    public int HistoryID { get; set; }

    // Foreign Keys
    public int ProductID { get; set; }

    [Required]
    public int AddedStocks { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [ForeignKey("ProductID")]
    public virtual Products Product { get; set; }
}
