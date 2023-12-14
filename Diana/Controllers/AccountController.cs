using Diana.Models;
using Diana.Services;
using Diana.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Diana.Controllers
{
	[AutoValidateAntiforgeryToken]
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM registerVM)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			AppUser user = new AppUser()
			{
				Name = registerVM.Name,
				Email = registerVM.Email,
				Surname = registerVM.Surname,
				UserName = registerVM.Username
			};
			var result = await _userManager.CreateAsync(user, registerVM.Password);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return View();
			}
			await _signInManager.SignInAsync(user, false);
			//await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());
			//SendMailService.SendMail(to:user.Email, name: user.Name);    ////////////////// for send mail 
			return RedirectToAction(nameof(Index), "Home");
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM, string? ReturnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			AppUser user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
				if (user == null)
				{
					ModelState.AddModelError("", "Username or password is incorrect ");
					return View();
				}
			}
            var result = _signInManager.CheckPasswordSignInAsync(user, loginVM.Password, true).Result;
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "  Try again later.");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username or password is incorrect.");
                return View();
            }
            await _signInManager.SignInAsync(user, loginVM.RememberMe);

            if (ReturnUrl != null && !ReturnUrl.Contains("Login"))
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction(nameof(Index), "Home");
        }
        public async  Task<IActionResult> Logout()
		{

            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
	}
}
