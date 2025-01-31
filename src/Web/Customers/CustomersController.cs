using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Customers;
using OrderManagementSystem.Contracts.Customers;
using OrderManagementSystem.Web.Accounting;

namespace OrderManagementSystem.Web.Customers;

[Authorize(Roles = ApplicationRoleNames.Manager)]
[ApiController]
[Route("api/v1/[controller]")]
public sealed class CustomersController : ControllerBase
{
    private readonly ICustomerService customerService;

    public CustomersController(ICustomerService customerService)
    {
        this.customerService = customerService;
    }
    
    [HttpGet]
    public async Task<IReadOnlyList<CustomerResponse>> GetCustomersAsync(
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        CancellationToken cancellationToken)
    {
        if (offset < 0 || limit < 1)
        {
            throw new ArgumentException($"Параметр offset не должен быть меньше нуля, limit должен быть больше нуля");
        }
        
        var customers = await customerService.GetAllCustomersAsync(limit ?? 10, offset ?? 0, cancellationToken);
        
        var customerResponses = customers
            .Select(x => x.MapToCustomerResponse())
            .ToList();
        
        return customerResponses;
    }
    
    [HttpGet("{customerId}")]
    public async Task<CustomerResponse> GetCustomerByIdAsync(
        [FromRoute] Guid customerId,
        CancellationToken cancellationToken)
    {
        var customer = await customerService.GetCustomerByIdAsync(customerId, cancellationToken);

        return customer.MapToCustomerResponse();
    }
    
    [HttpPost]
    public async Task<CustomerResponse> CreateCustomerAsync(
        [FromBody] CreateCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand
            (
                request.Name,
                request.Code,
                request.Address,
                request.Discount
            );
        
        var customer = await customerService.AddCustomerAsync(command, cancellationToken);
        
        return customer.MapToCustomerResponse();
    }
    
    [HttpPut("{customerId}")]
    public async Task<CustomerResponse> UpdateCustomerAsync(
        [FromRoute] Guid customerId,
        [FromBody] UpdateCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCustomerCommand
        (
            customerId,
            request.Name,
            request.Code,
            request.Address,
            request.Discount
        );
        
        var customer = await customerService.UpdateCustomerAsync(command, cancellationToken);
        
        return customer.MapToCustomerResponse();
    }
    
    [HttpDelete("{customerId}")]
    public async Task DeleteCustomerAsync(
        [FromRoute] Guid customerId,
        CancellationToken cancellationToken)
    {
        await customerService.DeleteCustomerAsync(customerId, cancellationToken);
    }
}