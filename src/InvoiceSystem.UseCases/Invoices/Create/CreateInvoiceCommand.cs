namespace InvoiceSystem.UseCases.Invoices.Create;

/// <summary>
/// Create a new Invoice with items.
/// </summary>
public record CreateInvoiceItemRequest(
  string ProductName,
  int Quantity,
  decimal UnitPrice,
  decimal TaxRate = 0,
  decimal DiscountPercentage = 0,
  string? Description = null);

public record CreateInvoiceCommand(
  string InvoiceNumber,
  DateTime InvoiceDate,
  string CustomerName,
  DateTime? DueDate,
  string? Notes,
  List<CreateInvoiceItemRequest> Items) : ICommand<Result<int>>;
