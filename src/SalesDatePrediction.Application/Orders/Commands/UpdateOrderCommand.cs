using System;
using MediatR;

namespace SalesDatePrediction.Application.Orders.Commands
{
    public record UpdateOrderCommand(
        int OrderId,
        int CustId,
        int EmployeeId,
        DateTime OrderDate,
        DateTime? RequiredDate,
        DateTime? ShippedDate,
        int ShipperId,
        decimal Freight,
        string ShipName,
        string ShipAddress,
        string ShipCity,
        string ShipRegion,
        string ShipPostalCode,
        string ShipCountry
    ) : IRequest<Unit>;
}
