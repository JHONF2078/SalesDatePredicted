// File: src/SalesDatePrediction.Application/Products/Queries/GetProductsQueryHandler.cs
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Application.Products.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Products.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Repository<Product>().GetAsync(
                selector: p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName
                }
            );

            return products.ToList();
        }
    }
}
