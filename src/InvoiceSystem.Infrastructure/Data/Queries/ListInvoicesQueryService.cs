using InvoiceSystem.UseCases.Invoices.List;

namespace InvoiceSystem.Infrastructure.Data.Queries;

public class ListInvoicesQueryService(AppDbContext _db) : IListInvoicesQueryService
{
  public async Task<IEnumerable<InvoiceListDTO>> ListAsync()
  {
    var result = await _db.Database.SqlQuery<InvoiceListDTO>(
      $@"SELECT
          Id,
          InvoiceNumber,
          InvoiceDate,
          CustomerName,
          TotalAmount,
          Status,
          DueDate
        FROM Invoices
        ORDER BY InvoiceDate DESC")
      .ToListAsync();

    return result;
  }
}
