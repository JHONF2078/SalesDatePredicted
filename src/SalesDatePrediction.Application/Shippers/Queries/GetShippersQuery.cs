using System.Collections.Generic;
using MediatR;
using SalesDatePrediction.Application.Shippers.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Shippers.Queries
{
    public record GetShippersQuery : IRequest<List<ShipperDto>>;
}