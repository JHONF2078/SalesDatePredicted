﻿using MediatR;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Application.Customers.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Customers.Queries
{
    public class GetCustomersWithOrderInfoQueryHandler : IRequestHandler<GetCustomersWithOrderInfoQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersWithOrderInfoQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerDto>> Handle(GetCustomersWithOrderInfoQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomerOrderInfoAsync();
        }
    }
}