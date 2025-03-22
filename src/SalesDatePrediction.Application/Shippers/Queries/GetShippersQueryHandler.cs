// File: src/SalesDatePrediction.Application/Shippers/Queries/GetShippersQueryHandler.cs
using MediatR;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Application.Shippers.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Shippers.Queries
{
    public class GetShippersQueryHandler : IRequestHandler<GetShippersQuery, List<ShipperDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetShippersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ShipperDto>> Handle(GetShippersQuery request, CancellationToken cancellationToken)
        {
            var shippers = await _unitOfWork.Repository<Shipper>()
                .GetAsync<ShipperDto>(
                    selector: s => new ShipperDto
                    {
                        ShipperId = s.ShipperId,
                        CompanyName = s.CompanyName
                    }
                );

            return shippers.ToList();
        }
    }
}
