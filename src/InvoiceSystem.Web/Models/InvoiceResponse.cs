namespace InvoiceSystem.Web.Models;

public class InvoiceResponse
{
  public int Id { get; set; }
  public string InvoiceNumber { get; set; } = default!;
  public DateTime InvoiceDate { get; set; }
  public string CustomerName { get; set; } = default!;
  public decimal TotalAmount { get; set; }
  public decimal TaxAmount { get; set; }
  public string Status { get; set; } = default!;
  public DateTime? DueDate { get; set; }
  public string? Notes { get; set; }
  public List<InvoiceItemResponse> Items { get; set; } = new();
}

public class InvoiceItemResponse
{
  public int Id { get; set; }
  public string ProductName { get; set; } = default!;
  public int Quantity { get; set; }
  public decimal UnitPrice { get; set; }
  public decimal LineTotal { get; set; }
  public decimal TaxRate { get; set; }
  public decimal DiscountPercentage { get; set; }
  public string? Description { get; set; }
}
