using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Orders.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            await _unitOfWork.Repository<Order>().DeleteAsync(order);
            await _unitOfWork.Complete();

            return Unit.Value;
        }
    }
}
