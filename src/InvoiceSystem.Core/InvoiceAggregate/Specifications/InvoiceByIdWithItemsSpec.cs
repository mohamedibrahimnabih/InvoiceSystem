namespace InvoiceSystem.Core.InvoiceAggregate.Specifications;

public class InvoiceByIdWithItemsSpec : Specification<Invoice>
{
  public InvoiceByIdWithItemsSpec(int invoiceId) =>
    Query
        .Where(invoice => invoice.Id == invoiceId)
        .Include(invoice => invoice.Items);
}
