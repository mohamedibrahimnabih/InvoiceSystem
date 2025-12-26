namespace InvoiceSystem.Core.InvoiceAggregate.Specifications;

public class InvoiceByIdSpec : Specification<Invoice>
{
  public InvoiceByIdSpec(int invoiceId) =>
    Query
        .Where(invoice => invoice.Id == invoiceId);
}
