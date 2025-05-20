using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

public class Transactions
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(50)]
    public string Subtotal { get; set; }

    [Required]
    [StringLength(50)]
    public string Cash { get; set; }

    [Required]
    [StringLength(50)]
    public string DiscountPercent { get; set; }

    [Required]
    [StringLength(50)]
    public string DiscountAmount { get; set; }

    [Required]
    [StringLength(50)]
    public string Total { get; set; }

    [Required]
    [StringLength(50)]
    public string Change { get; set; }

    [Required]
    [StringLength(50)]
    public string TransactionId { get; set; }
}