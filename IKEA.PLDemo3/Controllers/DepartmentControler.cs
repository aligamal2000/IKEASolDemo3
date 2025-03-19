using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.BILLDemo3.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;


namespace IKEA.PLDemo3.Controllers
{
    public class DepartmentController : Controller
    {

        #region Services
        private IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _departmentServices, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            logger = _logger;
            this.environment = environment;
        } 
        #endregion
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();
            return View(Departments);
        }

        #endregion
        #region Details
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = departmentServices.GetDepartmentByid(id.Value);
            if (department == null)
                return NotFound();
            return View(department);
        }

        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto1)
        {
            if (!ModelState.IsValid)
                return View(departmentDto1);

            var Message = string.Empty;

            try
            {
                // Ensure proper mapping
                var departmentModel = new IKEA.DALDemo3.Models.Departments.CreatedDepartmentDto
                {
                    Name = departmentDto1.Name,
                    Code = departmentDto1.Code,
                    Description = departmentDto1.Description,
                    CreationDate = departmentDto1.CreationDate
                };

                var Result = departmentServices.CreateDepartment(departmentModel);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));

                Message = "Department is not Created";
                ModelState.AddModelError(string.Empty, Message);
                return View(departmentDto1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Message = environment.IsDevelopment() ? ex.Message : "Error";
                ModelState.AddModelError(string.Empty, Message);
                return View(departmentDto1);
            }
        }

        #endregion
        #region Update
        [HttpGet] //get: /Department/Edit/10
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var Department = departmentServices.GetDepartmentByid(id.Value);

            if (Department is null)
                return NotFound();

            var MappedDepartment = new UpdatedDepartmentDto()
            {
                Id = Department.Id,
                Name = Department.Name,
                Code = Department.Code,
                Description = Department.Description,
                CreationDate = Department.CreationDate,
            };

            return View(Department);





        }
        [HttpPost]
        public IActionResult Edit(UpdatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return View(departmentDto);

            var Message = String.Empty;
            try
            {
              
                var Result = departmentServices.UpdateDepartment(departmentDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Department is not Updated";
            }

            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                Message = environment.IsDevelopment() ? ex.Message : "an error during update department";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(departmentDto);
        }
        #endregion
        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var Department = departmentServices.GetDepartmentByid(id.Value);

            if (Department is null)
                return NotFound();

            return View(Department);
        }
        [HttpPost]
        public IActionResult Delete(int Deptid)
        {
            var Message = string.Empty;
            try
            {
                var IsDeleted = departmentServices.DeleteDepartment(Deptid);

                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                Message = "Department is Not Deleted";
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                Message = environment.IsDevelopment()? ex.Message: "An error has benn occured";
                    
            }
            ModelState.AddModelError(string.Empty , Message);
            return RedirectToAction(nameof(Delete), new {id= Deptid });
}
                 

        #endregion


    }
}