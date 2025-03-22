using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Infrastructure.Persistence;
using SalesDatePrediction.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Customers.Queries.DTOs;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CustomerDto>> GetCustomerOrderInfoAsync()
        {
            var query = @"
                WITH OrderIntervals AS (
                    SELECT 
                        o.CustId,
                        o.OrderDate,
                        LAG(o.OrderDate) OVER (PARTITION BY o.CustId ORDER BY o.OrderDate) AS PreviousOrderDate
                    FROM 
                        Sales.Orders o
                )
                SELECT 
                    c.CustId,
                    c.CompanyName,
                    MAX(o.OrderDate) AS LastOrderDate,
                    DATEADD(DAY, AVG(DATEDIFF(DAY, oi.PreviousOrderDate, oi.OrderDate)), MAX(o.OrderDate)) AS PossibleNextOrderDate
                FROM 
                    Sales.Orders o
                JOIN 
                    Sales.Customers c ON o.CustId = c.CustId
                JOIN 
                    OrderIntervals oi ON o.CustId = oi.CustId AND o.OrderDate = oi.OrderDate
                WHERE 
                    oi.PreviousOrderDate IS NOT NULL
                GROUP BY 
                    c.CustId, c.CompanyName
                ORDER BY 
                    c.CompanyName";

            var customerOrderInfo = await _dbContext.Database.ExecuteSqlRawAsync(query);

            var result = new List<CustomerDto>();

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                _dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new CustomerDto
                        {
                            CustId = reader.GetInt32(0),
                            CompanyName = reader.GetString(1),
                            LastOrderDate = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            PossibleNextOrderDate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
                        });
                    }
                }
            }

            return result;
        }
    }
}
