using IKEA.BILLDemo3.Dto_s.Employees;
using IKEA.BILLDemo3.Services.DepartmentServices;
using IKEA.BILLDemo3.Services.EmployeeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PLDemo3.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices, IDepartmentServices departmentServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }

        [HttpGet]
        public async Task <IActionResult> Index(string search)
        {
            var employees = await employeeServices.GetAllEmployees(search);
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);

            try
            {
                var result = await employeeServices.CreateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Employee is not created");
                return View(employeeDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, environment.IsDevelopment() ? ex.Message : "An error occurred");
                return View(employeeDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var employee = await employeeServices.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpGet]
        [Authorize(Roles ="Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var employee = await employeeServices.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();

            var mappedEmployee = new UpdatedEmployeeDto
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
                ImageName = employee.ImageName
            };

            return View(mappedEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);

            try
            {
                var result = await employeeServices.UpdateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Employee is not updated");
                return View(employeeDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, environment.IsDevelopment() ? ex.Message : "An error occurred");
                return View(employeeDto);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var employee = employeeServices.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int empId)
        {
            try
            {
                var isDeleted = await employeeServices.DeleteEmployee(empId);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Employee is not deleted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, environment.IsDevelopment() ? ex.Message : "An error occurred");
            }

            return RedirectToAction(nameof(Delete), new { id = empId });
        }
    }
}
