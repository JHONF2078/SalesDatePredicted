// File: src/SalesDatePrediction.Application/Orders/Commands/CreateOrderCommandHandler.cs
using MediatR;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Application.Orders.Commands.DTOs;
using SalesDatePrediction.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace SalesDatePrediction.Application.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderWithDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderWithDetailsDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Verificar que el valor de discount esté dentro del rango permitido
            if (request.OrderDetail.Discount < 0 || request.OrderDetail.Discount > 1)
            {
                throw new ArgumentException("El valor de discount debe estar entre 0 y 1.");
            }

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

            var orderDetail = order.OrderDetails.First();

            return new OrderWithDetailsDto(
                order.EmpId,
                order.ShipperId,
                order.ShipName,
                order.ShipAddress,
                order.ShipCity,
                order.OrderDate,
                order.RequiredDate,
                order.ShippedDate,
                order.Freight,
                order.ShipCountry,
                new OrderDetailDto(
                    order.OrderId,
                    orderDetail.ProductId,
                    orderDetail.UnitPrice,
                    orderDetail.Qty,
                    orderDetail.Discount
                )
            );
        }
    }
}