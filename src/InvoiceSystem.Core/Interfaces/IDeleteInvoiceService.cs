namespace InvoiceSystem.Core.Interfaces;

public interface IDeleteInvoiceService
{
  // This service and method exist to provide a place in which to fire domain events
  // when deleting this aggregate root entity
  public Task<Result> DeleteInvoice(int invoiceId);
}
