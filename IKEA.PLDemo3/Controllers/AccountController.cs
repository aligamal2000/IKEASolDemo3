using IKEA.DALDemo3.Models.identity;
using IKEA.PLDemo3.Models.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PLDemo3.Controllers
{
    public class AccountController : Controller
    {

        #region Services
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser>signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #endregion

        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var User = await userManager.FindByNameAsync(signUpViewModel.UserName);
            if (User is not null)
            {
                ModelState.AddModelError(nameof(signUpViewModel.UserName), "This Username is already in Use for another user.");
                return View(signUpViewModel);
            }

            User = new ApplicationUser()
            {
                FName = signUpViewModel.FirstName,
                LName = signUpViewModel.LastName,
                UserName = signUpViewModel.UserName,
                Email = signUpViewModel.Email,
                IsAgree = signUpViewModel.IsAgree,
            };
       var Result = await     userManager.CreateAsync(User,signUpViewModel.Password);
            if(Result.Succeeded)
                return RedirectToAction(nameof(LogIn));
            foreach(var error in Result.Errors)
                ModelState.AddModelError(string.Empty,error.Description);
            return View(signUpViewModel);
            
        }

        #endregion
        #region LogIn
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel logInViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var User = await userManager.FindByEmailAsync(logInViewModel.Email);

            if (User is not null)
            {
                var result = await signInManager.PasswordSignInAsync(User, logInViewModel.Password, logInViewModel.RememberMe, false);

                if (result.IsNotAllowed)
                    ModelState.AddModelError(string.Empty, "Your Account Is Not Confirmed");

                if (result.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "Your Account Is Locked!");

                if (result.Succeeded)
                    return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ModelState.AddModelError(String.Empty, "invalid login Attempt !");
            return View(logInViewModel);
        }
        #endregion
        #region SignOut
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
        #endregion

    }
}
