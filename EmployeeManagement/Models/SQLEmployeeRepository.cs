using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;
        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }



        public Employee AddEmployee(Employee employee)
        {
            this.context.Employees.Add(employee);
            this.context.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee emp = this.context.Employees.Find(id);
            if (emp != null)
            {
                this.context.Employees.Remove(emp);
                this.context.SaveChanges();
            }
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return this.context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            return this.context.Employees.Find(Id);
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {

            // New way of updating in ASP NET CORE.
            var emp = this.context.Employees.Attach(employeeChanges);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return employeeChanges;

        }


        //    Employee emp = this.context.Employees.Find(employeeChanges.Id);

        //    if(emp != null)
        //    {
        //        emp.Name = employeeChanges.Name;
        //        emp.Department = employeeChanges.Department;
        //        emp.Email = employeeChanges.Email;
        //    }

        //    return employeeChanges;

        //}
    }
}
