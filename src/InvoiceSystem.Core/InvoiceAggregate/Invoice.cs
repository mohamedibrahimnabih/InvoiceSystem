namespace InvoiceSystem.Core.InvoiceAggregate;

public class Invoice : EntityBase, IAggregateRoot
{
  // Private parameterless constructor for EF Core
  private Invoice()
  {
    _items = new List<InvoiceItem>();
  }

  // Public constructor for creating new invoices
  public Invoice(string invoiceNumber, DateTime invoiceDate, string customerName)
  {
    InvoiceNumber = Guard.Against.NullOrEmpty(invoiceNumber, nameof(invoiceNumber));
    InvoiceDate = invoiceDate;
    CustomerName = Guard.Against.NullOrEmpty(customerName, nameof(customerName));
    Status = InvoiceStatus.Draft;
    _items = new List<InvoiceItem>();
  }

  // Properties
  public string InvoiceNumber { get; private set; } = default!;
  public DateTime InvoiceDate { get; private set; }
  public string CustomerName { get; private set; } = default!;
  public decimal TotalAmount { get; private set; }
  public decimal TaxAmount { get; private set; }
  public InvoiceStatus Status { get; private set; } = InvoiceStatus.Draft;
  public DateTime? DueDate { get; private set; }
  public string? Notes { get; private set; }

  // Collection of items
  private readonly List<InvoiceItem> _items;
  public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();

  // Methods for encapsulation and business logic
  public Invoice UpdateCustomerName(string customerName)
  {
    CustomerName = Guard.Against.NullOrEmpty(customerName, nameof(customerName));
    return this;
  }

  public Invoice UpdateInvoiceDate(DateTime invoiceDate)
  {
    InvoiceDate = invoiceDate;
    return this;
  }

  public Invoice SetDueDate(DateTime? dueDate)
  {
    DueDate = dueDate;
    return this;
  }

  public Invoice SetNotes(string? notes)
  {
    Notes = notes;
    return this;
  }

  public Invoice AddItem(InvoiceItem item)
  {
    Guard.Against.Null(item, nameof(item));
    _items.Add(item);
    RecalculateTotals();
    return this;
  }

  public Invoice RemoveItem(int itemId)
  {
    var item = _items.FirstOrDefault(i => i.Id == itemId);
    if (item != null)
    {
      _items.Remove(item);
      RecalculateTotals();
    }
    return this;
  }

  public Invoice ClearItems()
  {
    _items.Clear();
    RecalculateTotals();
    return this;
  }

  public Invoice MarkAsIssued()
  {
    Status = InvoiceStatus.Issued;
    return this;
  }

  public Invoice MarkAsPaid()
  {
    Status = InvoiceStatus.Paid;
    return this;
  }

  public Invoice MarkAsDraft()
  {
    Status = InvoiceStatus.Draft;
    return this;
  }

  private void RecalculateTotals()
  {
    TotalAmount = _items.Sum(item => item.LineTotal);
    TaxAmount = _items.Sum(item => item.CalculateTax());
  }
}
