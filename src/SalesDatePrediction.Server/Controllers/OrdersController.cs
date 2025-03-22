using Microsoft.AspNetCore.Mvc;
using MediatR;
using SalesDatePrediction.Application.Orders.Commands;
using System;
using System.Threading.Tasks;
using SalesDatePrediction.Application.Orders.Queries;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Application.Orders.Queries.DTOs;

namespace SalesDatePrediction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ByCustomer/{customerId}")]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersByCustomer(int customerId)
        {
            var orders = await _mediator.Send(new GetOrdersByCustomerQuery(customerId));
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return Ok(orderId);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderCommand command)
        //{
        //    if (id != command.OrderId)
        //    {
        //        return BadRequest();
        //    }

        //    await _mediator.Send(command);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOrder(int id)
        //{
        //    await _mediator.Send(new DeleteOrderCommand(id));
        //    return NoContent();
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOrderById(int id)
        //{
        //    // Implementar lógica de consulta
        //    return Ok();
        //}
    }
}