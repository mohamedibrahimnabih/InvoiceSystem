using InvoiceSystem.Core.InvoiceAggregate;
using InvoiceSystem.Core.InvoiceAggregate.Events;
using InvoiceSystem.Core.Interfaces;

namespace InvoiceSystem.Core.Services;

/// <summary>
/// Domain service for deleting invoices.
/// This service fires domain events when an invoice is deleted.
/// </summary>
/// <param name="_repository"></param>
/// <param name="_mediator"></param>
/// <param name="_logger"></param>
public class DeleteInvoiceService(IRepository<Invoice> _repository,
  IMediator _mediator,
  ILogger<DeleteInvoiceService> _logger) : IDeleteInvoiceService
{
  public async Task<Result> DeleteInvoice(int invoiceId)
  {
    _logger.LogInformation("Deleting Invoice {invoiceId}", invoiceId);
    Invoice? aggregateToDelete = await _repository.GetByIdAsync(invoiceId);
    if (aggregateToDelete == null) return Result.NotFound();

    await _repository.DeleteAsync(aggregateToDelete);
    var domainEvent = new InvoiceDeletedEvent(invoiceId);
    await _mediator.Publish(domainEvent);

    return Result.Success();
  }
}
