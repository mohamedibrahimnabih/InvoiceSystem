namespace InvoiceSystem.Core.InvoiceAggregate;

public class InvoiceStatus : SmartEnum<InvoiceStatus>
{
  public static readonly InvoiceStatus Draft = new(nameof(Draft), 1);
  public static readonly InvoiceStatus Issued = new(nameof(Issued), 2);
  public static readonly InvoiceStatus Paid = new(nameof(Paid), 3);

  protected InvoiceStatus(string name, int value) : base(name, value) { }
}
