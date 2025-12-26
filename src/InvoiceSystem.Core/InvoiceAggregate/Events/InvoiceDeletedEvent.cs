namespace InvoiceSystem.Core.InvoiceAggregate.Events;

/// <summary>
/// A domain event that is dispatched whenever an invoice is deleted.
/// The DeleteInvoiceService is used to dispatch this event.
/// </summary>
internal sealed class InvoiceDeletedEvent(int invoiceId) : DomainEventBase
{
  public int InvoiceId { get; init; } = invoiceId;
}
