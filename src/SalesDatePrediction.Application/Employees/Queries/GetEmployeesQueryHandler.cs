// File: src/SalesDatePrediction.Application/Employees/Queries/GetEmployeesQueryHandler.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Application.Employees.Queries.DTOs;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Employees.Queries
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployeesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employeeDtos = await _unitOfWork.Repository<Employee>()
                .GetAsync(
                    selector: e => new EmployeeDto
                    {
                        EmpId = e.EmpId,
                        FullName = e.FirstName + " " + e.LastName
                    });

            return employeeDtos.ToList();
        }
    }
}
