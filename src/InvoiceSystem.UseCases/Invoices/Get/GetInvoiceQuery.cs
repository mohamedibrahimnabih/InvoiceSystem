namespace InvoiceSystem.UseCases.Invoices.Get;

public record GetInvoiceQuery(int InvoiceId) : IQuery<Result<InvoiceDTO>>;
