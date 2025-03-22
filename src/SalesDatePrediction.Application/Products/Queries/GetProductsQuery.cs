using System.Collections.Generic;
using MediatR;
using SalesDatePrediction.Application.Products.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Products.Queries
{
    public record GetProductsQuery : IRequest<List<ProductDto>>;
}