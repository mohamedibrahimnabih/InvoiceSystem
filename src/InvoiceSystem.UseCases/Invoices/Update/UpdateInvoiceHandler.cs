using InvoiceSystem.Core.InvoiceAggregate;
using InvoiceSystem.Core.InvoiceAggregate.Specifications;

namespace InvoiceSystem.UseCases.Invoices.Update;

public class UpdateInvoiceHandler(IRepository<Invoice> _repository)
  : ICommandHandler<UpdateInvoiceCommand, Result<InvoiceDTO>>
{
  public async Task<Result<InvoiceDTO>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
  {
    var spec = new InvoiceByIdWithItemsSpec(request.InvoiceId);
    var invoice = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

    if (invoice == null) return Result.NotFound();

    // Update basic properties
    invoice.UpdateCustomerName(request.CustomerName)
           .UpdateInvoiceDate(request.InvoiceDate)
           .SetDueDate(request.DueDate)
           .SetNotes(request.Notes);

    // Update status
    if (request.Status == "Draft") invoice.MarkAsDraft();
    else if (request.Status == "Issued") invoice.MarkAsIssued();
    else if (request.Status == "Paid") invoice.MarkAsPaid();

    // Update items - clear and re-add
    invoice.ClearItems();
    foreach (var itemRequest in request.Items)
    {
      var item = new InvoiceItem(
        itemRequest.ProductName,
        itemRequest.Quantity,
        itemRequest.UnitPrice,
        itemRequest.TaxRate,
        itemRequest.DiscountPercentage,
        itemRequest.Description);

      invoice.AddItem(item);
    }

    await _repository.UpdateAsync(invoice, cancellationToken);

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
