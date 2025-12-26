namespace InvoiceSystem.UseCases.Invoices;

public record InvoiceDTO(
  int Id,
  string InvoiceNumber,
  DateTime InvoiceDate,
  string CustomerName,
  decimal TotalAmount,
  decimal TaxAmount,
  string Status,
  DateTime? DueDate,
  string? Notes,
  List<InvoiceItemDTO> Items);
