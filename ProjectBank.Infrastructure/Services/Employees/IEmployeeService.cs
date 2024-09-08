using Microsoft.AspNetCore.Mvc;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Infrastructure.Services.Employees
{
    public interface IEmployeeService
    {
        Task<ActionResult<List<Employee>>> Get(string? search, string? sortItem, string? sortOrder);
        Task<Employee> Post(Employee customer);
        Task<Employee> Update(Guid id, Employee requestModel);
        Task<Employee> Delete(Guid id);
    }
}
