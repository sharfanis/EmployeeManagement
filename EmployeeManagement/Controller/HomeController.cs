using EmployeeManagement.DTO_s;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    [Route("[controller]/[action]")]
public class HomeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;

    // This is called Constructor Injection .Injecting the repository inside the Home Controller constructor.
    public HomeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

        // Added Routes for Routing through Attribute
        [Route("")]
        [Route("~/")]
        [Route("~/Home")]
        //[Route("Home/Index")]
        public ViewResult Index()
    {
        var model =  _employeeRepository.GetAllEmployees();
        return View("~/Views/Home/Index.cshtml",model);

    }
        [Route("{id?}")]
        public ViewResult Details(int? id)
    {
        //Employee employee = _employeeRepository.GetEmployee(id);

        // Use of View Data for passing values from Controller to View.
        // ViewData["Employee"] = employee;
        // ViewData["PageTitle"] = "Employee Details";

        // Use of View Bag
        //ViewBag.Employee = employee;
        //ViewBag.PageTitile = "Employee Details";



        //return View("~/MyViews/Test.cshtml");

        // Relative path for views
        // return View("../../MyViews/Test");

        // Passing the values using strongly typed view.
        //ViewBag.PageTitle = "Employee Details";
        //return View(employee);


        // Use of DTO now.

        HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
        {
            Employee = _employeeRepository.GetEmployee(id??1),
            PageTitle = "Employee Details Homie"
        };

        return View(homeDetailsViewModel);
    }
}
}
