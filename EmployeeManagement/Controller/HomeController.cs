using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        // This is called Constructor Injection .Injecting the repository inside the Home Controller constructor.
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Employee Index()
        {
           return _employeeRepository.GetEmployee(2);
        }

        public ViewResult Details()
        {
            Employee employee = _employeeRepository.GetEmployee(1);
            return View("~/MyViews/Test.cshtml");
        }
    }
}
