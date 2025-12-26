using InvoiceSystem.Core.InvoiceAggregate;
using InvoiceSystem.Core.InvoiceAggregate.Specifications;

namespace InvoiceSystem.UseCases.Invoices.Get;

/// <summary>
/// Get an invoice by ID with all its items.
/// </summary>
public class GetInvoiceHandler(IReadRepository<Invoice> _repository)
  : IQueryHandler<GetInvoiceQuery, Result<InvoiceDTO>>
{
  public async Task<Result<InvoiceDTO>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
  {
    var spec = new InvoiceByIdWithItemsSpec(request.InvoiceId);
    var invoice = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

    if (invoice == null) return Result.NotFound();

    var items = invoice.Items.Select(item => new InvoiceItemDTO(
      item.Id,
      item.ProductName,
      item.Quantity,
      item.UnitPrice,
      item.LineTotal,
      item.TaxRate,
      item.DiscountPercentage,
      item.Description)).ToList();

    return new InvoiceDTO(
      invoice.Id,
      invoice.InvoiceNumber,
      invoice.InvoiceDate,
      invoice.CustomerName,
      invoice.TotalAmount,
      invoice.TaxAmount,
      invoice.Status.Name,
      invoice.DueDate,
      invoice.Notes,
      items);
  }
}
