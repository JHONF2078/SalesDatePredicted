using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesDatePrediction.Domain.Entities;


namespace SalesDatePrediction.Application.Common.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);

    }
}
