using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Shippers.Queries;
using SalesDatePrediction.Application.Shippers.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShippersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShipperDto>>> GetShippers()
        {
            var shippers = await _mediator.Send(new GetShippersQuery());
            return Ok(shippers);
        }
    }
}