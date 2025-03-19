using IKEA.BILLDemo3.Services.EmployeeServices;
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
        public IActionResult Index()
        {
            return View();
        } 
        #endregion
    }
}
