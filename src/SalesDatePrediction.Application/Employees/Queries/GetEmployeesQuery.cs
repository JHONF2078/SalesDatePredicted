using System.Collections.Generic;
using MediatR;
using SalesDatePrediction.Application.Employees.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Employees.Queries
{
    public record GetEmployeesQuery : IRequest<List<EmployeeDto>>;
}