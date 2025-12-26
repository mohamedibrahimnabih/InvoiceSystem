using System.ComponentModel.DataAnnotations;

namespace InvoiceSystem.Web.Models;

public class UpdateInvoiceRequest
{
  [Required]
  [MaxLength(100)]
  public string CustomerName { get; set; } = default!;

  [Required]
  public DateTime InvoiceDate { get; set; }

  public DateTime? DueDate { get; set; }

  [MaxLength(500)]
  public string? Notes { get; set; }

  [Required]
  public string Status { get; set; } = "Draft";

  [Required]
  [MinLength(1)]
  public List<CreateInvoiceItemRequest> Items { get; set; } = new();
}
