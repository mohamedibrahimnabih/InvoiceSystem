namespace InvoiceSystem.Core.InvoiceAggregate.Events;

/// <summary>
/// A domain event that is dispatched whenever an invoice is created.
/// </summary>
internal sealed class InvoiceCreatedEvent(int invoiceId, string invoiceNumber) : DomainEventBase
{
  public int InvoiceId { get; init; } = invoiceId;
  public string InvoiceNumber { get; init; } = invoiceNumber;
}
