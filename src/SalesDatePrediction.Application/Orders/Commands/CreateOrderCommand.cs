// File: src/SalesDatePrediction.Application/Orders/Commands/CreateOrderCommand.cs
using System;
using MediatR;
using SalesDatePrediction.Application.Orders.Commands.DTOs;

namespace SalesDatePrediction.Application.Orders.Commands
{
    public record CreateOrderCommand(
        int CustId,
        int EmpId,
        DateTime? OrderDate,
        DateTime? RequiredDate,
        DateTime? ShippedDate,
        int ShipperId,
        decimal Freight,
        string ShipName,
        string ShipAddress,
        string ShipCity,
        string ShipCountry,
        CreateOrderDetailDto OrderDetail
    ) : IRequest<OrderWithDetailsDto>;

    public record CreateOrderDetailDto(
        int ProductId,
        decimal UnitPrice,
        int Qty,
        float Discount
    );
}