using GudaIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GudaIdentity.Controllers
{
    public class CuentasController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signManager;


        public CuentasController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signManager)
        {
            this.userManager = userManager;
            this.signManager = signManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registro()
        {
            RegistroViewModel registoVM = new RegistroViewModel();
            return View(registoVM);
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel rgViewModel)
        {
            if (ModelState.IsValid)
            {


                var usuario = new AppUsuario
                {
                    UserName = rgViewModel.Email,
                    Email = rgViewModel.Email,
                    Nombre = rgViewModel.Nombre,
                    Url = rgViewModel.Url,
                    CodigoPais = rgViewModel.CodigoPais,
                    Telefono = rgViewModel.Telefono,
                    Pais = rgViewModel.Pais,
                    Ciudad = rgViewModel.Ciudad,
                    Direccion = rgViewModel.Direccion,
                    FechaNacimiento = rgViewModel.FechaNacimiento,
                    Estado = rgViewModel.Estado


                };

                var resultado = await userManager.CreateAsync(usuario, rgViewModel.Password);

                if (resultado.Succeeded)
                {

                    await signManager.SignInAsync(usuario, isPersistent: false);

                    return RedirectToAction("Index", "Home");


                }
                ValidarErrores(resultado);


            }
            return View(rgViewModel);
        }


        // manejador de errores
        private void ValidarErrores(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }

        //Mostrar formulario de login
        [HttpGet]
        public IActionResult Acceso()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Acceso (AccesoViewModel accViewModel)
        {
            if (ModelState.IsValid)
            {
                var resultado = await signManager.PasswordSignInAsync(accViewModel.Email, accViewModel.Password, accViewModel.RemmemberMe,lockoutOnFailure: false);
                if(resultado.Succeeded) 
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Acceso Invalido");
                    return View(accViewModel);
                }                     
            }
            return View(accViewModel);
        }
    }
}