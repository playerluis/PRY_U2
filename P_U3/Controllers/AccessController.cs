using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P_U3.Context;
using P_U3.Models;

namespace P_U3.Controllers;

public class AccessController : Controller
{
    private readonly BddCitasMedicasContext _context;

    public AccessController(BddCitasMedicasContext context)
    {
        _context = context;
    }
    
    public IActionResult  Login()
    {
        var claimsPrincipal = HttpContext.User;
        if (claimsPrincipal.Identity is { IsAuthenticated: true })
            return RedirectToAction("Index", "Home");
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel  loginModel)
    {
        if (string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Contrasenia))
        {
            ViewData["Error"] = "Email y contraseña son requeridos";
            return View("Login");
        }
        
        var usuario =  await _context.Usuario
            .FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Contrasenia == loginModel.Contrasenia);
        if (usuario == null)
        {
            ViewData["Error"] = "Email y/o contraseña incorrectos";
            return View("Login");
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Name, usuario.Nombre),
            new(ClaimTypes.Email, usuario.Email)
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = true
        };
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            claimsPrincipal, 
            authProperties
            );
        
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Access");
    }
}