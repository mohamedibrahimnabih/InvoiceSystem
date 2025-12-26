namespace InvoiceSystem.Core.InvoiceAggregate.Events;

/// <summary>
/// A domain event that is dispatched whenever an invoice is updated.
/// </summary>
internal sealed class InvoiceUpdatedEvent(int invoiceId) : DomainEventBase
{
  public int InvoiceId { get; init; } = invoiceId;
}
