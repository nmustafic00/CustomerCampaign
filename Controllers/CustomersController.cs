using CustomerCampaign.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{customerId}/preview")]
    public async Task<IActionResult> GetCustomerPreview(string customerId)
    {
        var customer = await _customerService.GetCustomerPreviewAsync(customerId);

        if (customer == null)
            return NotFound(new { message = "Customer not found" });

        return Ok(customer);
    }
}
