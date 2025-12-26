namespace InvoiceSystem.UseCases.Invoices.List;

/// <summary>
/// Represents a service that will actually fetch the necessary data
/// Typically implemented in Infrastructure
/// </summary>
public interface IListInvoicesQueryService
{
  Task<IEnumerable<InvoiceListDTO>> ListAsync();
}
