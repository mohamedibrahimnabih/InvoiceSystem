namespace InvoiceSystem.UseCases.Invoices.List;

public class ListInvoicesHandler(IListInvoicesQueryService _query)
  : IQueryHandler<ListInvoicesQuery, Result<IEnumerable<InvoiceListDTO>>>
{
  public async Task<Result<IEnumerable<InvoiceListDTO>>> Handle(ListInvoicesQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.ListAsync();

    return Result.Success(result);
  }
}
