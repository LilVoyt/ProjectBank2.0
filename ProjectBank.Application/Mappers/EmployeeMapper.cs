using ProjectBank.Application.Models;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Mappers
{
    internal class EmployeeMapper
    {
        public Employee GetEmployee(EmployeeRequestModel requestModel)
        {
            var employee = new Employee();
            employee.Id = Guid.NewGuid();
            employee.Name = requestModel.Name;
            employee.LastName = requestModel.LastName;
            employee.Country = requestModel.Country;
            employee.Phone = requestModel.Phone;
            employee.Email = requestModel.Email;

            return employee;
        }


        public EmployeeRequestModel GetRequestModel(Employee employee)
        {
            var requestModel = new EmployeeRequestModel();
            requestModel.Name = employee.Name;
            requestModel.LastName = employee.LastName;
            requestModel.Country = employee.Country;
            requestModel.Phone = employee.Phone;
            requestModel.Email = employee.Email;

            return requestModel;
        }


        public Employee PutRequestModelInEmployee(Employee res, EmployeeRequestModel employee)
        {
            res.Name = employee.Name;
            res.LastName = employee.LastName;
            res.Country = employee.Country;
            res.Phone = employee.Phone;
            res.Email = employee.Email;

            return res;
        }

        public List<EmployeeRequestModel> GetRequestModels(List<Employee> employees)
        {
            return employees.Select(employee => GetRequestModel(employee)).ToList();
        }
    }
}
