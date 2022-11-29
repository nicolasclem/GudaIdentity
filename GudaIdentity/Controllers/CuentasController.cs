using GudaIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GudaIdentity.Controllers
{
    public class CuentasController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signManager;
        private readonly IEmailSender emailSender;

        public CuentasController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signManager , IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signManager = signManager;
            this.emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registro(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            RegistroViewModel registoVM = new RegistroViewModel();
            return View(registoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel rgViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");
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

                    //return RedirectToAction("Index", "Home");
                    return LocalRedirect(returnUrl);

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
        public IActionResult Acceso(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acceso (AccesoViewModel accViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var resultado = await signManager.PasswordSignInAsync(accViewModel.Email, accViewModel.Password, accViewModel.RemmemberMe,lockoutOnFailure: true);
                if(resultado.Succeeded) 
                {
                    //return RedirectToAction("Index", "Home");

                    return LocalRedirect(returnUrl);
                }
                if (resultado.IsLockedOut)
                {
                    return View("Bloqueado");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Acceso Invalido");
                    return View(accViewModel);
                }                     
            }
            return View(accViewModel);
        }

        // logout  de la applicacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalirAplicacion()
        {
            await signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        //metodo olvido contraseña


        [HttpGet]
        public IActionResult OlvidoPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OlvidoPassword(OlvidoPasswordViewModel opassViewModel) 
        {
            if(ModelState.IsValid)
            {
                var usuario = await  userManager.FindByEmailAsync(opassViewModel.Email); 
                
                if(usuario == null)
                {
                    return RedirectToAction("ConfirmacionOlvidoPass");
                }

                var codigo = await userManager.GeneratePasswordResetTokenAsync(usuario);
                var urlRetorno = Url.Action("ResetPassword","Cuentas", new {userId=usuario.Id, code=codigo}, protocol:HttpContext.Request.Scheme);

                await emailSender.SendEmailAsync(opassViewModel.Email,"Recuperar Contraseña - GudaIdentity",
                    "Recupere su  contraseña dando click aqui: <a href=\""+ urlRetorno +"\">Enlace</a>");

                return RedirectToAction("ConfirmacionOlvidoPass");
            
            }

            return View(opassViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmacionOlvidoPass()
        {
            return View();
        }

        //funcionalidad para recuperar  contraseña
        [HttpGet]
        public IActionResult ResetPassword(string code=null)
        {
            return code==null?View("Error"):View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(RecuperaPasswordViewModel recuViewModel)
        {
            if (ModelState.IsValid) 
            {
                 var usuario= await userManager.FindByEmailAsync(recuViewModel.Email);
                 if( usuario == null) 
                 {
                    return RedirectToAction("OlvidoPassword");
                 }
                var resultado = await userManager.ResetPasswordAsync(usuario, recuViewModel.Code, recuViewModel.Password);

                if (resultado.Succeeded) 
                {
                    return RedirectToAction("ConfirmacionRecuperaPassword");
                }
                ValidarErrores(resultado);
            }
            return View(recuViewModel);
        }

        [HttpGet]
        public IActionResult ConfirmacionRecuperaPassword()
        {
            return View();
        }

    }
}