using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Customers.Queries.DTOs
{
    public class CustomerDto
    {
        public int CustId { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public DateTime? PossibleNextOrderDate { get; set; }
    }
}
