using InvoiceSystem.Core.InvoiceAggregate;

namespace InvoiceSystem.UseCases.Invoices.Create;

public class CreateInvoiceHandler(IRepository<Invoice> _repository)
  : ICommandHandler<CreateInvoiceCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateInvoiceCommand request,
    CancellationToken cancellationToken)
  {
    // Create invoice
    var invoice = new Invoice(
      request.InvoiceNumber,
      request.InvoiceDate,
      request.CustomerName);

    if (request.DueDate.HasValue)
    {
      invoice.SetDueDate(request.DueDate.Value);
    }

    if (!string.IsNullOrEmpty(request.Notes))
    {
      invoice.SetNotes(request.Notes);
    }

    // Add items
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

    var createdInvoice = await _repository.AddAsync(invoice, cancellationToken);

    return createdInvoice.Id;
  }
}
