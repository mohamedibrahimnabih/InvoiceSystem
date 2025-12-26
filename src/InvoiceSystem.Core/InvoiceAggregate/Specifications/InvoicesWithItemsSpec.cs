namespace InvoiceSystem.Core.InvoiceAggregate.Specifications;

public class InvoicesWithItemsSpec : Specification<Invoice>
{
  public InvoicesWithItemsSpec() =>
    Query
        .Include(invoice => invoice.Items);
}
