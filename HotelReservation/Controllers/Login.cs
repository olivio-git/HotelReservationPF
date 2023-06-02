using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservation;
using HotelReservation.Models;

namespace HotelReservation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;
        public LoginController(AppDbContext context, JwtService jwtService, IConfiguration configuration)
        {
            _context = context;
            _jwtService = jwtService;
            _configuration = configuration;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            Console.WriteLine(usuario);
            if (usuario.Correo != null && usuario.Contrasena != null)
            {
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == usuario.Correo);
                if (user == null)
                {
                    Console.WriteLine("Primer if");
                    ModelState.AddModelError(string.Empty, "Correo incorrecto");
                    return View(usuario);
                }
                if (user.Contrasena != usuario.Contrasena)
                {
                    Console.WriteLine("Segundo if");
                    Console.WriteLine("Contraseña incorrecta");
                    return View(usuario);
                }
                var token = _jwtService.GenerateJwtToken(user);
                GlobalVariables.AgregarUsuario(user);
                GlobalVariables.Agregar(user.Correo); //guardamos el token en nuestra variable global
                if(!string.IsNullOrEmpty(GlobalVariables.StateGlobal)
                    && GlobalVariables.usuario.Idrol == 1)
                {
                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    return RedirectToAction("HomeClient");
                }
            }
            return View(usuario);
        }
        public IActionResult DeleteSession()
        {
            Console.WriteLine("Click en boton");
            GlobalVariables.Eliminar();
            GlobalVariables.EliminarUsuario();
            GlobalVariables.EliminarPersona();
            return RedirectToAction("HomePage", "Home");

        }
        [HttpPost]
        public IActionResult Cargar(string getToken)
        {
            Console.WriteLine(getToken);
            Usuario user = _context.Usuarios.Where(u => u.Correo == getToken).First();
            if(user!=null){
                var persona = _context.Personas.Where(p => p.Id == user.Idpersona).First();
                GlobalVariables.AgregarUsuario(user);
                GlobalVariables.AgregarPersona(persona);
                return RedirectToAction("HomePage", "Home");
            }
            return RedirectToAction("HomePage", "Home");
        }
        public IActionResult HomeClient()
        {
            return RedirectToAction("ViewHabitations", "Habitacion");
        }
    }
}
