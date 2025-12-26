using Ardalis.Result;
using InvoiceSystem.UseCases.Invoices.Create;
using InvoiceSystem.UseCases.Invoices.Delete;
using InvoiceSystem.UseCases.Invoices.Get;
using InvoiceSystem.UseCases.Invoices.List;
using InvoiceSystem.UseCases.Invoices.Update;
using InvoiceSystem.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController(IMediator mediator, ILogger<InvoicesController> logger)
    : ControllerBase
{
  private readonly IMediator _mediator = mediator;
  private readonly ILogger<InvoicesController> _logger = logger;

  /// <summary>
  /// Create a new invoice with items
  /// </summary>
  /// <param name="request">Invoice creation request</param>
  /// <param name="cancellationToken">Cancellation token</param>
  /// <returns>Created invoice ID</returns>
  [Microsoft.AspNetCore.Mvc.HttpPost]
  [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<int>> CreateInvoice(
      [Microsoft.AspNetCore.Mvc.FromBody] CreateInvoiceRequest request,
      CancellationToken cancellationToken)
  {
    _logger.LogInformation("Creating invoice {InvoiceNumber}", request.InvoiceNumber);

    var items = request.Items.Select(i => new UseCases.Invoices.Create.CreateInvoiceItemRequest(
        i.ProductName,
        i.Quantity,
        i.UnitPrice,
        i.TaxRate,
        i.DiscountPercentage,
        i.Description)).ToList();

    var command = new CreateInvoiceCommand(
        request.InvoiceNumber,
        request.InvoiceDate,
        request.CustomerName,
        request.DueDate,
        request.Notes,
        items);

    var result = await _mediator.Send(command, cancellationToken);

    if (result.IsSuccess)
    {
      return CreatedAtAction(
          nameof(GetInvoiceById),
          new { id = result.Value },
          result.Value);
    }

    return BadRequest(result.Errors);
  }

  /// <summary>
  /// Get invoice by ID with all items
  /// </summary>
  /// <param name="id">Invoice ID</param>
  /// <param name="cancellationToken">Cancellation token</param>
  /// <returns>Invoice details</returns>
  [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
  [ProducesResponseType(typeof(InvoiceResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<InvoiceResponse>> GetInvoiceById(
      int id,
      CancellationToken cancellationToken)
  {
    _logger.LogInformation("Getting invoice {InvoiceId}", id);

    var query = new GetInvoiceQuery(id);
    var result = await _mediator.Send(query, cancellationToken);

    if (result.IsSuccess)
    {
      var dto = result.Value;
      var response = new InvoiceResponse
      {
        Id = dto.Id,
        InvoiceNumber = dto.InvoiceNumber,
        InvoiceDate = dto.InvoiceDate,
        CustomerName = dto.CustomerName,
        TotalAmount = dto.TotalAmount,
        TaxAmount = dto.TaxAmount,
        Status = dto.Status,
        DueDate = dto.DueDate,
        Notes = dto.Notes,
        Items = dto.Items.Select(i => new InvoiceItemResponse
        {
          Id = i.Id,
          ProductName = i.ProductName,
          Quantity = i.Quantity,
          UnitPrice = i.UnitPrice,
          LineTotal = i.LineTotal,
          TaxRate = i.TaxRate,
          DiscountPercentage = i.DiscountPercentage,
          Description = i.Description
        }).ToList()
      };

      return Ok(response);
    }

    return NotFound();
  }

  /// <summary>
  /// Get all invoices (without items for performance)
  /// </summary>
  /// <param name="cancellationToken">Cancellation token</param>
  /// <returns>List of invoices</returns>
  [Microsoft.AspNetCore.Mvc.HttpGet]
  [ProducesResponseType(typeof(IEnumerable<InvoiceListDTO>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<InvoiceListDTO>>> GetAllInvoices(
      CancellationToken cancellationToken)
  {
    _logger.LogInformation("Getting all invoices");

    var query = new ListInvoicesQuery(null, null);
    var result = await _mediator.Send(query, cancellationToken);

    if (result.IsSuccess)
    {
      return Ok(result.Value);
    }

    return BadRequest(result.Errors);
  }

  /// <summary>
  /// Update an existing invoice
  /// </summary>
  /// <param name="id">Invoice ID</param>
  /// <param name="request">Update request</param>
  /// <param name="cancellationToken">Cancellation token</param>
  /// <returns>Updated invoice</returns>
  [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
  [ProducesResponseType(typeof(InvoiceResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<InvoiceResponse>> UpdateInvoice(
      int id,
      [Microsoft.AspNetCore.Mvc.FromBody] UpdateInvoiceRequest request,
      CancellationToken cancellationToken)
  {
    _logger.LogInformation("Updating invoice {InvoiceId}", id);

    var items = request.Items.Select(i => new UseCases.Invoices.Create.CreateInvoiceItemRequest(
        i.ProductName,
        i.Quantity,
        i.UnitPrice,
        i.TaxRate,
        i.DiscountPercentage,
        i.Description)).ToList();

    var command = new UpdateInvoiceCommand(
        id,
        request.CustomerName,
        request.InvoiceDate,
        request.DueDate,
        request.Notes,
        request.Status,
        items);

    var result = await _mediator.Send(command, cancellationToken);

    if (result.IsSuccess)
    {
      var dto = result.Value;
      var response = new InvoiceResponse
      {
        Id = dto.Id,
        InvoiceNumber = dto.InvoiceNumber,
        InvoiceDate = dto.InvoiceDate,
        CustomerName = dto.CustomerName,
        TotalAmount = dto.TotalAmount,
        TaxAmount = dto.TaxAmount,
        Status = dto.Status,
        DueDate = dto.DueDate,
        Notes = dto.Notes,
        Items = dto.Items.Select(i => new InvoiceItemResponse
        {
          Id = i.Id,
          ProductName = i.ProductName,
          Quantity = i.Quantity,
          UnitPrice = i.UnitPrice,
          LineTotal = i.LineTotal,
          TaxRate = i.TaxRate,
          DiscountPercentage = i.DiscountPercentage,
          Description = i.Description
        }).ToList()
      };

      return Ok(response);
    }

    if (result.Status == ResultStatus.NotFound)
    {
      return NotFound();
    }

    return BadRequest(result.Errors);
  }

  /// <summary>
  /// Delete an invoice
  /// </summary>
  /// <param name="id">Invoice ID</param>
  /// <param name="cancellationToken">Cancellation token</param>
  /// <returns>No content on success</returns>
  [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteInvoice(
      int id,
      CancellationToken cancellationToken)
  {
    _logger.LogInformation("Deleting invoice {InvoiceId}", id);

    var command = new DeleteInvoiceCommand(id);
    var result = await _mediator.Send(command, cancellationToken);

    if (result.IsSuccess)
    {
      return NoContent();
    }

    if (result.Status == ResultStatus.NotFound)
    {
      return NotFound();
    }

    return BadRequest(result.Errors);
  }
}
