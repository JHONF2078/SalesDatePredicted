using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Api.Controllers;
using SalesDatePrediction.Application.Customers.Queries;
using SalesDatePrediction.Application.Customers.Queries.DTOs;
using MediatR;

namespace SalesDatePrediction.Tests.controllers
{
    public class CustomersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CustomersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetCustomersWithOrderInfo_ReturnsFilteredCustomers_WhenCompanyNameProvided()
        {
            // Arrange
            var companyName = "AHPOP";
            var expected = new List<CustomerDto>
            {
                new CustomerDto { CustId = 72, CustomerName = "Customer AHPOP", LastOrderDate = DateTime.Parse("2025-03-22"), PossibleNextOrderDate = DateTime.Parse("2027-04-05") }
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetCustomersWithOrderInfoQuery>(q => q.CustomerName == companyName), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetCustomersWithOrderInfo(companyName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actual = Assert.IsType<List<CustomerDto>>(okResult.Value);
            Assert.Single(actual);
            Assert.Equal("Customer AHPOP", actual[0].CustomerName);
        }

        [Fact]
        public async Task GetCustomersWithOrderInfo_ReturnsAllCustomers_WhenNoCompanyNameProvided()
        {
            // Arrange
            var expected = new List<CustomerDto>
            {
                new CustomerDto { CustId = 72, CustomerName = "Customer AHPOP", LastOrderDate = DateTime.Parse("2025-03-22"), PossibleNextOrderDate = DateTime.Parse("2027-04-05") },
                new CustomerDto { CustId = 58, CustomerName = "Customer AHXHT", LastOrderDate = DateTime.Parse("2008-05-05"), PossibleNextOrderDate = DateTime.Parse("2008-08-28") }
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetCustomersWithOrderInfoQuery>(q => q.CustomerName == null), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetCustomersWithOrderInfo(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actual = Assert.IsType<List<CustomerDto>>(okResult.Value);
            Assert.Equal(2, actual.Count);
        }


    }
}
