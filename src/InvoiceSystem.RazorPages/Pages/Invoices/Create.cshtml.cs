using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceSystem.RazorPages.Pages.Invoices;

public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(ILogger<CreateModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        // Set default invoice date to today
    }
}
