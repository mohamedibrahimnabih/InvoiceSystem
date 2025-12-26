using InvoiceSystem.Core.Interfaces;

namespace InvoiceSystem.UseCases.Invoices.Delete;

public class DeleteInvoiceHandler(IDeleteInvoiceService _deleteInvoiceService)
  : ICommandHandler<DeleteInvoiceCommand, Result>
{
  public async Task<Result> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken) =>
    await _deleteInvoiceService.DeleteInvoice(request.InvoiceId);
}
