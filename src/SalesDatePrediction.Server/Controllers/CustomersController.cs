using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Customers.Queries;
using SalesDatePrediction.Application.Customers.Queries.DTOs;


namespace SalesDatePrediction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("WithOrderInfo")]
        public async Task<ActionResult<List<CustomerDto>>> GetCustomersWithOrderInfo([FromQuery] string? customerName)
        {
            var customers = await _mediator.Send(new GetCustomersWithOrderInfoQuery(customerName));
            return Ok(customers);
        }
    }
}
