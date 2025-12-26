namespace InvoiceSystem.UseCases.Invoices.List;

public record InvoiceListDTO(
  int Id,
  string InvoiceNumber,
  DateTime InvoiceDate,
  string CustomerName,
  decimal TotalAmount,
  string Status,
  DateTime? DueDate);
