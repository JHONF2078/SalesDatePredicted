using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SalesDatePrediction.Application.Customers.Queries;
using SalesDatePrediction.Application.Customers.Queries.DTOs;
using SalesDatePrediction.Application.Common.Interfaces;

namespace SalesDatePrediction.Tests.handlers
{
    public class GetCustomersWithOrderInfoQueryHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly GetCustomersWithOrderInfoQueryHandler _handler;

        public GetCustomersWithOrderInfoQueryHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _handler = new GetCustomersWithOrderInfoQueryHandler(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFilteredResults_WhenCompanyNameIsProvided()
        {
            // Arrange
            var companyName = "AHPOP";
            var customers = new List<CustomerDto>
            {
                new CustomerDto { CustId = 1, CustomerName = "Customer AHPOP" },
                new CustomerDto { CustId = 2, CustomerName = "Customer Other" }
            };

            _customerRepositoryMock
                .Setup(repo => repo.GetCustomerOrderInfoAsync())
                .ReturnsAsync(customers);

            var query = new GetCustomersWithOrderInfoQuery(companyName);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Single(result);
            Assert.Contains("AHPOP", result[0].CustomerName);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllResults_WhenCompanyNameIsNull()
        {
            // Arrange
            var customers = new List<CustomerDto>
            {
                new CustomerDto { CustId = 1, CustomerName = "Customer A" },
                new CustomerDto { CustId = 2, CustomerName = "Customer B" }
            };

            _customerRepositoryMock
                .Setup(repo => repo.GetCustomerOrderInfoAsync())
                .ReturnsAsync(customers);

            var query = new GetCustomersWithOrderInfoQuery(null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
