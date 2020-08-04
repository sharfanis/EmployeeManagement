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
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };

            return View(employeeEditViewModel);
        }


        [HttpPost]

        public IActionResult Edit(EmployeeEditViewModel model)
        {
            // IAction Result is parent of ViewResult and RedirectToActionResult.

            if (ModelState.IsValid) // This for checking if the validation on field is present.
            {

                Employee employee = _employeeRepository.GetEmployee(model.Id);

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                // To check if a new photo is uploaded.
                if (model.Photos != null)
                {
                    // To delete the old picture if a new photo is upploaded.
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    employee.PhotoPath = ProcessUploadedFile(model);
                }

                _employeeRepository.UpdateEmployee(employee);
                return RedirectToAction("index", new { id = employee.Id });

            }

            return View();


        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photos != null && model.Photos.Count > 0)
            {
                foreach (IFormFile photo in model.Photos)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using(var fileStream = new FileStream(filePath , FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                   
                }
            }

            return uniqueFileName;
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
                string uniqueFileName = ProcessUploadedFile(model);


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

             //throw new Exception("Error in details view"); //This is only to trigger the error messages.


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

            Employee employee = _employeeRepository.GetEmployee(id.Value);

            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);

            }


        HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
        {
            Employee = employee,
            PageTitle = "Employee Details Homie"
        };

        return View(homeDetailsViewModel);
    }
}
}
