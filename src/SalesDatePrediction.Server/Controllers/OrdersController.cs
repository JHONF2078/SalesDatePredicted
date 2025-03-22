using Microsoft.AspNetCore.Mvc;
using MediatR;
using SalesDatePrediction.Application.Orders.Commands;
using System;
using System.Threading.Tasks;
using SalesDatePrediction.Application.Orders.Queries;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Application.Orders.Queries.DTOs;
using SalesDatePrediction.Application.Orders.Commands.DTOs;

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
        public async Task<ActionResult<OrderWithDetailsDto>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderWithDetails = await _mediator.Send(command);
            return Ok(orderWithDetails);
        }
    }
}