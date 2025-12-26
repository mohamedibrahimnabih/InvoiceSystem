using InvoiceSystem.Core.Interfaces;
using InvoiceSystem.Core.Services;
using InvoiceSystem.Infrastructure.Data;
using InvoiceSystem.Infrastructure.Data.Queries;
using InvoiceSystem.UseCases.Contributors.List;
using InvoiceSystem.UseCases.Invoices.List;


namespace InvoiceSystem.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("SqliteConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options =>
     options.UseSqlite(connectionString));

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
           .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
           .AddScoped<IListContributorsQueryService, ListContributorsQueryService>()
           .AddScoped<IDeleteContributorService, DeleteContributorService>()
           .AddScoped<IListInvoicesQueryService, ListInvoicesQueryService>()
           .AddScoped<IDeleteInvoiceService, DeleteInvoiceService>();


    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
