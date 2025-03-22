using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Orders.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            order.CustId = request.CustId;
            order.EmpId = request.EmployeeId;
            order.OrderDate = request.OrderDate;
            order.RequiredDate = request.RequiredDate;
            order.ShippedDate = request.ShippedDate;
            order.ShipperId = request.ShipperId;
            order.Freight = request.Freight;
            order.ShipName = request.ShipName;
            order.ShipAddress = request.ShipAddress;
            order.ShipCity = request.ShipCity;
            order.ShipRegion = request.ShipRegion;
            order.ShipPostalCode = request.ShipPostalCode;
            order.ShipCountry = request.ShipCountry;

            await _unitOfWork.Repository<Order>().UpdateAsync(order);
            await _unitOfWork.Complete();

            return Unit.Value;
        }
    }
}
