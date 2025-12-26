using InvoiceSystem.Core.InvoiceAggregate;

namespace InvoiceSystem.Infrastructure.Data.Config;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
  public void Configure(EntityTypeBuilder<Invoice> builder)
  {
    builder.Property(p => p.InvoiceNumber)
        .HasMaxLength(50)
        .IsRequired();

    builder.Property(p => p.CustomerName)
        .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
        .IsRequired();

    builder.Property(p => p.TotalAmount)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

    builder.Property(p => p.TaxAmount)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

    builder.Property(p => p.Notes)
        .HasMaxLength(500);

    builder.Property(x => x.Status)
      .HasConversion(
          x => x.Value,
          x => InvoiceStatus.FromValue(x));

    // One-to-many relationship with InvoiceItems
    builder.HasMany<InvoiceItem>("_items")
        .WithOne()
        .HasForeignKey(item => item.InvoiceId)
        .OnDelete(DeleteBehavior.Cascade);

    // Unique constraint on InvoiceNumber
    builder.HasIndex(p => p.InvoiceNumber)
        .IsUnique();
  }
}
