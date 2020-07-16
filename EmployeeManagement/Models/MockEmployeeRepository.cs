using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() {Id = 1 , Name = "Shabih" , Department = Dept.IT, Email = "shabih.sharfani@gmail.com"},
                new Employee() {Id = 2 , Name = "Natalia" , Department = Dept.HR, Email = "nsc@gmail.com"},
                new Employee() {Id = 3 , Name = "Subuhi" , Department = Dept.Medico, Email = "subuhi@gmail.com"}

            };

        }

        public Employee AddEmployee(Employee employee)
        {
           employee.Id =_employeeList.Max(e => e.Id) + 1;

            _employeeList.Add(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }



    }
}
