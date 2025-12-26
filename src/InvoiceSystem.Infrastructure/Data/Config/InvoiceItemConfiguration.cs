using InvoiceSystem.Core.InvoiceAggregate;

namespace InvoiceSystem.Infrastructure.Data.Config;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
  public void Configure(EntityTypeBuilder<InvoiceItem> builder)
  {
    builder.Property(p => p.ProductName)
        .HasMaxLength(200)
        .IsRequired();

    builder.Property(p => p.UnitPrice)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

    builder.Property(p => p.LineTotal)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

    builder.Property(p => p.TaxRate)
        .HasColumnType("decimal(5,2)")
        .IsRequired();

    builder.Property(p => p.DiscountPercentage)
        .HasColumnType("decimal(5,2)")
        .IsRequired();

    builder.Property(p => p.Description)
        .HasMaxLength(500);
  }
}
