using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Orders.Commands.DTOs
{
    public record OrderWithDetailsDto(
        int EmpId,
        int ShipperId,
        string ShipName,
        string ShipAddress,
        string ShipCity,
        DateTime? OrderDate,
        DateTime? RequiredDate,
        DateTime? ShippedDate,
        decimal Freight,
        string ShipCountry,
        OrderDetailDto OrderDetail
    );

    public record OrderDetailDto(
        int OrderId,
        int ProductId,
        decimal UnitPrice,
        int Qty,
        float Discount
    );
}
