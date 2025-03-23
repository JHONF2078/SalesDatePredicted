﻿using MediatR;
using SalesDatePrediction.Application.Customers.Queries.DTOs;
using System.Collections.Generic;

namespace SalesDatePrediction.Application.Customers.Queries
{
    public record GetCustomersWithOrderInfoQuery(string? CompanyName) : IRequest<List<CustomerDto>>;
}