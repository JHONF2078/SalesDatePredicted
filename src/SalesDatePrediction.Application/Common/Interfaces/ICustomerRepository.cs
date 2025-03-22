using SalesDatePrediction.Application.Customers.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Common.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<List<CustomerDto>> GetCustomerOrderInfoAsync();
    }
}
