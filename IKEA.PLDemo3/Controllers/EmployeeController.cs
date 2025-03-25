using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.BILLDemo3.Dto_s.Employees;
using IKEA.BILLDemo3.Services.DepartmentServices;
using IKEA.BILLDemo3.Services.EmployeeServices;
using IKEA.DALDemo3.Models.Empolyees;
using IKEA.DALDemo3.Persistance.Data.Migrations;
using IKEA.DALDemo3.Persistance.Repositories.Empoyees;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PLDemo3.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services D1
        private readonly IEmpolyeeServices employeeServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;


        public EmployeeController(IEmpolyeeServices employeeServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]

        public IActionResult Index()
        {
            var Empployees = employeeServices.GetAllEmployees();

            return View(Empployees);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);

            var Message = string.Empty;

            try
            {


                var Result = employeeServices.CreateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));

                Message = "Department is not Created";
                ModelState.AddModelError(string.Empty, Message);
                return View(employeeDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Message = environment.IsDevelopment() ? ex.Message : "Error";
                ModelState.AddModelError(string.Empty, Message);
                return View(employeeDto);
            }
        }

        #endregion
        #region Details
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee = employeeServices.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = employeeServices.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            var MappedEmployee = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Age = employee.Age,
                Email = employee.Email,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,

                HiringDate = employee.HiringDate,
                IsActive = employee.IsActive,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,

            };

            return View(MappedEmployee);

        }
        [HttpPost]
        public IActionResult Edit(UpdatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);

            var Message = string.Empty;

            try
            {


                var Result = employeeServices.UpdateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));

                Message = "Department is not Created";
                ModelState.AddModelError(string.Empty, Message);
                return View(employeeDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Message = environment.IsDevelopment() ? ex.Message : "Error";
                ModelState.AddModelError(string.Empty, Message);
                return View(employeeDto);
            }
        }

        #endregion
        #region Delete
        [HttpGet]

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = employeeServices.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);
        }
        [HttpPost]
        public IActionResult Delete(int EmpId)
        {
            var Message = string.Empty;
            try
            {
                var IsDeleted = employeeServices.DeleteEmployee(EmpId);

                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                Message = "Department is Not Deleted";
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                Message = environment.IsDevelopment() ? ex.Message : "An error has benn occured";

            }
            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new { id = EmpId });
        }


        #endregion
    }
}
