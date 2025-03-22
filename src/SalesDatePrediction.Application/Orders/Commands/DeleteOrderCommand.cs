using System;
using MediatR;

namespace SalesDatePrediction.Application.Orders.Commands
{
    public record DeleteOrderCommand(int OrderId) : IRequest<Unit>;
}