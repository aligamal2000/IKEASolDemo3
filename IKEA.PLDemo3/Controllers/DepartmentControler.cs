using AutoMapper;
using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.BILLDemo3.Services.DepartmentServices;
using IKEA.PLDemo3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;


namespace IKEA.PLDemo3.Controllers
{
    public class DepartmentController : Controller
    {

        #region Services
        private IDepartmentServices departmentServices;
        private UpdatedDepartmentDto deparmentDto;
        private readonly IMapper mapper;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _departmentServices,IMapper mapper, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            this.mapper = mapper;
            logger = _logger;
            this.environment = environment;
        } 
        #endregion
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();
            //ViewData["Message"] = "Hello From ViewData";
            //ViewBag.Message = "Helllo From ViewBag";
            //string Name = ViewBag.Message;
            //ViewBag.Message = 1;
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
  
        public IActionResult Create(CreateEditDepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var Message = string.Empty;

            try
            {
                // Ensure proper mapping
                var departmentModel = new IKEA.DALDemo3.Models.Departments.CreatedDepartmentDto
                {
                    Name = departmentVM.Name,
                    Code = departmentVM.Code,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate
                };
                //AutoMapper
                var departmentDto = mapper.Map<CreateEditDepartmentVM, CreatedDepartmentDto>(departmentVM);

                //var departmentDto = new CreatedDepartmentDto()
                //{
                //    Name = departmentVM.Name,
                //    Code = departmentVM.Code,
                //    Description = departmentVM.Description,
                //    CreationDate = departmentVM.CreationDate,

                //};


                var Result = departmentServices.CreateDepartment(departmentModel);
                if (Result > 0)
                    TempData["Message"] = $"{departmentDto.Name}Department Is Created";
                    return RedirectToAction(nameof(Index));

                Message = "Department is not Created";
                ModelState.AddModelError(string.Empty, Message);
                return View(departmentVM);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Message = environment.IsDevelopment() ? ex.Message : "Error";
                ModelState.AddModelError(string.Empty, Message);
                return View(departmentVM);
            }
        }

        #endregion
        #region Update
        [HttpGet] // GET: /Department/Edit/10
  
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = departmentServices.GetDepartmentByid(id.Value);

            if (department is null)
                return NotFound();
            var MappedDepartment = mapper.Map<DepartmentDetailsDto, CreateEditDepartmentVM>(department);

            //var mappedDepartment = new CreateEditDepartmentVM()
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    Code = department.Code,
            //    Description = department.Description,
            //    CreationDate = department.CreationDate,
            //};

            return View(MappedDepartment); // Ensure the view receives UpdatedDepartmentDto
        }





        
        [HttpPost]

        public IActionResult Edit(CreateEditDepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var Message = String.Empty;
            try
            {
              var departmentDto = mapper.Map<CreateEditDepartmentVM,UpdatedDepartmentDto>(departmentVM);
                //var deparmentDto = new UpdatedDepartmentDto()
                //{
                //    Name = departmentVM.Name,
                //    Code = departmentVM.Code,
                //    Description = departmentVM.Description,
                //    CreationDate = departmentVM.CreationDate,
                //};
                var Result = departmentServices.UpdateDepartment(deparmentDto);
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
            return View(departmentVM);
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
        [ValidateAntiForgeryToken]
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