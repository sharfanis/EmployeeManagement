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
                new Employee() {Id = 1 , Name = "Shabih" , Department = "IT", Email = "shabih.sharfani@gmail.com"},
                new Employee() {Id = 2 , Name = "Natalia" , Department = "HR", Email = "nsc@gmail.com"},
                new Employee() {Id = 3 , Name = "Subuhi" , Department = "Medic", Email = "subuhi@gmail.com"}

            };

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
