namespace InvoiceSystem.UseCases.Invoices;

public record InvoiceItemDTO(
  int Id,
  string ProductName,
  int Quantity,
  decimal UnitPrice,
  decimal LineTotal,
  decimal TaxRate,
  decimal DiscountPercentage,
  string? Description);
