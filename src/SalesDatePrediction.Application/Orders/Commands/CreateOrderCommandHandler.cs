// File: src/SalesDatePrediction.Application/Orders/Commands/CreateOrderCommandHandler.cs
using MediatR;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                CustId = request.CustId,
                EmpId = request.EmpId,
                OrderDate = request.OrderDate,
                RequiredDate = request.RequiredDate,
                ShippedDate = request.ShippedDate,
                ShipperId = request.ShipperId,
                Freight = request.Freight,
                ShipName = request.ShipName,
                ShipAddress = request.ShipAddress,
                ShipCity = request.ShipCity,
                ShipRegion = request.ShipRegion,
                ShipPostalCode = request.ShipPostalCode,
                ShipCountry = request.ShipCountry,
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        ProductId = request.OrderDetail.ProductId,
                        UnitPrice = request.OrderDetail.UnitPrice,
                        Qty = request.OrderDetail.Qty,
                        Discount = request.OrderDetail.Discount
                    }
                }
            };

            // Agregar la nueva orden al repositorio
            await _unitOfWork.Repository<Order>().AddAsync(order);

            // Confirmar la transacción
            await _unitOfWork.Complete();

            return order.OrderId;
        }
    }
}