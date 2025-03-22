using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Domain.Entities
{
    public  class Customer
    {
        public int CustId { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        //public DateTime? LastOrderDate { get; set; }
        //public DateTime? PossibleNextOrderDate { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
