using EducationalPlatform.Models;
using EducationalPlatform.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.Controllers
{
    public class LoginController(UserManager<UserModel> userManager, SignInManager<UserModel> userSignin) : Controller
    {
        private readonly UserManager<UserModel> _userManager = userManager;
        private readonly SignInManager<UserModel> _userSignin = userSignin;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, $"Login inválido. Cheque os campos Email e Senha!");
                return View("Index");
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, $"{nameof(model.Email)} ou Senha inválidos!");
                    return View("Index");
                }

                var result = await _userSignin.CheckPasswordSignInAsync(user, model.Password, true);

                if (!result.Succeeded)
                {
                    await _userManager.AccessFailedAsync(user);
                    ModelState.AddModelError(string.Empty, $"{nameof(model.Email)} ou Senha inválidos!");

                    if (result.IsLockedOut)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Limite máximo de tentativas excedido. Sua conta foi bloqueada temporariamente!");
                        return View("Index");
                    }

                    return View("Index");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema interno!";
                return View("Index");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = new UserModel(model.FullName, model.BirthDate, model.Email, model.Phone);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível efetuar o cadastro. Verifique os campos novamente!");
                    return View(model);
                }

                await _userManager.AddToRoleAsync(user, "User");
                var resultLogin = await _userSignin.CheckPasswordSignInAsync(user, model.Password, false);

                if (!resultLogin.Succeeded)
                    throw new Exception();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema interno!";
                return View("Register");
            }
        }
    }
}
