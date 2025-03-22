using MediatR;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Application.Orders.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Orders.Queries
{
    public class GetOrdersByCustomerQueryHandler : IRequestHandler<GetOrdersByCustomerQuery, List<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersByCustomerQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderDto>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            //linq query
            var orders = await _unitOfWork.Repository<Order>()
                .GetAsync(
                    predicate: o => o.CustId == request.CustId,
                    orderBy: q => q.OrderBy(o => o.OrderId),
                    disableTracking: true
                );

            var orderDtos = orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                RequiredDate = o.RequiredDate,
                ShippedDate = o.ShippedDate,
                ShipName = o.ShipName,
                ShipAddress = o.ShipAddress,
                ShipCity = o.ShipCity
            }).ToList();

            return orderDtos;
        }
    }
}
