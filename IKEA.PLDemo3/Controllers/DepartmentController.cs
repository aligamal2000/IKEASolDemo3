using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.BILLDemo3.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace IKEA.PLDemo3.Controllers
{
    public class DepartmentController : Controller
    {

        private IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _departmentServices,ILogger<DepartmentController>_logger,IWebHostEnvironment environment)
            {
            departmentServices =  _departmentServices;
            logger = _logger;
            this.environment = environment;
        }
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
                var Result = departmentServices.CreateDepartment(departmentDto1);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    Message = "Department is not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto1);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                if (environment.IsDevelopment())
                {
                    Message = ex.Message;
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto1);

                }
                else
                {
                    Message = "Error";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto1);
                }


            }

        } 
        #endregion

    }
}
