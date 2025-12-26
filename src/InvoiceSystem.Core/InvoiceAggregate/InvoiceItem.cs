namespace InvoiceSystem.Core.InvoiceAggregate;

public class InvoiceItem : EntityBase
{
  // Private parameterless constructor for EF Core
  private InvoiceItem() { }

  // Public constructor
  public InvoiceItem(
    string productName,
    int quantity,
    decimal unitPrice,
    decimal taxRate = 0,
    decimal discountPercentage = 0,
    string? description = null)
  {
    ProductName = Guard.Against.NullOrEmpty(productName, nameof(productName));
    Quantity = Guard.Against.NegativeOrZero(quantity, nameof(quantity));
    UnitPrice = Guard.Against.Negative(unitPrice, nameof(unitPrice));
    TaxRate = Guard.Against.OutOfRange(taxRate, nameof(taxRate), 0, 100);
    DiscountPercentage = Guard.Against.OutOfRange(discountPercentage, nameof(discountPercentage), 0, 100);
    Description = description;
    CalculateLineTotal();
  }

  public string ProductName { get; private set; } = default!;
  public int Quantity { get; private set; }
  public decimal UnitPrice { get; private set; }
  public decimal LineTotal { get; private set; }
  public decimal TaxRate { get; private set; }
  public decimal DiscountPercentage { get; private set; }
  public string? Description { get; private set; }

  // Foreign key
  public int InvoiceId { get; private set; }

  public InvoiceItem UpdateQuantity(int quantity)
  {
    Quantity = Guard.Against.NegativeOrZero(quantity, nameof(quantity));
    CalculateLineTotal();
    return this;
  }

  public InvoiceItem UpdateUnitPrice(decimal unitPrice)
  {
    UnitPrice = Guard.Against.Negative(unitPrice, nameof(unitPrice));
    CalculateLineTotal();
    return this;
  }

  public InvoiceItem UpdateTaxRate(decimal taxRate)
  {
    TaxRate = Guard.Against.OutOfRange(taxRate, nameof(taxRate), 0, 100);
    return this;
  }

  public InvoiceItem UpdateDiscountPercentage(decimal discountPercentage)
  {
    DiscountPercentage = Guard.Against.OutOfRange(discountPercentage, nameof(discountPercentage), 0, 100);
    CalculateLineTotal();
    return this;
  }

  private void CalculateLineTotal()
  {
    var subtotal = Quantity * UnitPrice;
    var discount = subtotal * (DiscountPercentage / 100);
    LineTotal = subtotal - discount;
  }

  public decimal CalculateTax()
  {
    return LineTotal * (TaxRate / 100);
  }
}
