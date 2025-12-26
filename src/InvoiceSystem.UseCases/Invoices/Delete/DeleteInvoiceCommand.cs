namespace InvoiceSystem.UseCases.Invoices.Delete;

public record DeleteInvoiceCommand(int InvoiceId) : ICommand<Result>;
