using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RealTime_ChatApp.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RandomNumberGenerator randomNumberGenerator;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (ModelState.IsValid)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = model.Username,
                Email = model.Email,
                CreatedAt = DateTime.Now
            };

            // Criar um hash seguro para a senha do usuário
            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(user, model.Password);

            // Atribuir a senha hashada ao usuário
            user.PasswordHash = hashedPassword;

            // Chamar o método CreateAsync para criar o usuário
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                // Se o usuário foi criado com sucesso, fazer login
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            // Se houver erros no resultado da criação, adicionar ao ModelState
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // Se o ModelState não for válido, retornar a View com o modelo para exibir os erros
        return View(model);
    }


    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
