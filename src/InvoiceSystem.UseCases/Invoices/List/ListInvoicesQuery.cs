namespace InvoiceSystem.UseCases.Invoices.List;

public record ListInvoicesQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<InvoiceListDTO>>>;
