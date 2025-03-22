using System;
using MediatR;

namespace SalesDatePrediction.Application.Orders.Commands
{
    public record CreateOrderCommand(
        int CustId,
        int EmpId,
        DateTime OrderDate,
        DateTime RequiredDate,
        DateTime ShippedDate,
        int ShipperId,
        decimal Freight,
        string ShipName,
        string ShipAddress,
        string ShipCity,
        string ShipRegion,
        string ShipPostalCode,
        string ShipCountry,
        CreateOrderDetailDto OrderDetail
    ) : IRequest<int>;

    public record CreateOrderDetailDto(
        int ProductId,
        decimal UnitPrice,
        int Qty,
        float Discount
    );
}
