using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Subuhi Sharfani",
                    Department = Dept.Medico,
                    Email = "Subuhi.sharfani@gmail.com"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Natalia Sharfani",
                    Department = Dept.HR,
                    Email = "Natalia.sharfani@gmail.com"
                }

                );
        }

    }
}
