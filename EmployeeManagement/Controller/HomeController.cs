using EmployeeManagement.DTO_s;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    [Route("[controller]/[action]")]
public class HomeController : Controller
{
       private readonly IEmployeeRepository _employeeRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

        //private readonly IHostingEnvironment hostingEnvironment;

        // This is called Constructor Injection .Injecting the repository inside the Home Controller constructor.
        public HomeController(IEmployeeRepository employeeRepository , Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        _employeeRepository = employeeRepository;
   
            this.hostingEnvironment = hostingEnvironment;
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
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult Create(Employee employee) 
        //{
        //// IAction Result is parent of ViewResult and RedirectToActionResult.

        //    if (ModelState.IsValid) // This for checking if the validation on field is present.
        //    {
        //        Employee newEmployee = _employeeRepository.AddEmployee(employee);
        //        return RedirectToAction("details", new { id = newEmployee.Id });
        //    }

        //    return View();


        //}

        public IActionResult Create(EmployeeCreateViewModel model)
        {
            // IAction Result is parent of ViewResult and RedirectToActionResult.

            if (ModelState.IsValid) // This for checking if the validation on field is present.
            {
                string uniqueFileName = null;

                if(model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (IFormFile photo in model.Photos)
                    {

                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.AddEmployee(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });

            }

            return View();


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
