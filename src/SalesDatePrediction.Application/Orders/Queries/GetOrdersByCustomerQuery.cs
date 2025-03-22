using MediatR;
using SalesDatePrediction.Application.Orders.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Orders.Queries
{
    public record GetOrdersByCustomerQuery(int CustId) : IRequest<List<OrderDto>>;
}