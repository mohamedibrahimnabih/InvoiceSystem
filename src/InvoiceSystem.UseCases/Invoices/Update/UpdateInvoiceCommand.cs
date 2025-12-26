using InvoiceSystem.UseCases.Invoices.Create;

namespace InvoiceSystem.UseCases.Invoices.Update;

public record UpdateInvoiceCommand(
  int InvoiceId,
  string CustomerName,
  DateTime InvoiceDate,
  DateTime? DueDate,
  string? Notes,
  string Status,
  List<CreateInvoiceItemRequest> Items) : ICommand<Result<InvoiceDTO>>;
