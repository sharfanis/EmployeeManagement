using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class AppDbContext : IdentityDbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }

        // By this we can seed our employee table with dummy data.
        //protected override void OnModelCreating(ModelBuilder builder)
        //{

        //    // Either you can put stuff here or Make new ModelBuilder Extenstion class.

        //    //modelBuilder.Entity<Employee>().HasData(
        //    //    new Employee
        //    //    {
        //    //        Id = 1,
        //    //        Name = "Subuhi Sharfani",
        //    //        Department = Dept.Medico,
        //    //        Email = "Subuhi.sharfani@gmail.com"
        //    //    },
        //    //    new Employee {
        //    //        Id = 2,
        //    //        Name = "Natalia Sharfani",
        //    //        Department = Dept.HR,
        //    //        Email = "Natalia.sharfani@gmail.com"
        //    //    }

        //    //    );



        //    // Since we have put stuff in ModelBuilderExtenstion we can use the Seed Method.
        //     base.OnModelCreating(builder);
        //     builder.Seed();

        //}




    }
}
