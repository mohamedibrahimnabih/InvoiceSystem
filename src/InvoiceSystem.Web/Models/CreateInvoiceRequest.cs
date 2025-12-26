using System.ComponentModel.DataAnnotations;

namespace InvoiceSystem.Web.Models;

public class CreateInvoiceItemRequest
{
  [Required]
  [MaxLength(200)]
  public string ProductName { get; set; } = default!;

  [Range(1, int.MaxValue)]
  public int Quantity { get; set; }

  [Range(0, double.MaxValue)]
  public decimal UnitPrice { get; set; }

  [Range(0, 100)]
  public decimal TaxRate { get; set; }

  [Range(0, 100)]
  public decimal DiscountPercentage { get; set; }

  [MaxLength(500)]
  public string? Description { get; set; }
}

public class CreateInvoiceRequest
{
  [Required]
  [MaxLength(50)]
  public string InvoiceNumber { get; set; } = default!;

  [Required]
  public DateTime InvoiceDate { get; set; }

  [Required]
  [MaxLength(100)]
  public string CustomerName { get; set; } = default!;

  public DateTime? DueDate { get; set; }

  [MaxLength(500)]
  public string? Notes { get; set; }

  [Required]
  [MinLength(1, ErrorMessage = "At least one invoice item is required")]
  public List<CreateInvoiceItemRequest> Items { get; set; } = new();
}
