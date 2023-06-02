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
    public class RegistroController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RegistroController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RegistroPersona(Persona persona)
        {
            if(ModelState.IsValid){
                var person = await _context.Personas.AddAsync(persona);
                await _context.SaveChangesAsync();
                GlobalVariables.AgregarPosiblePersona(person.Entity);
                return RedirectToAction("Usuario", "Registro");
            }
            return RedirectToAction("Index", "Registro");
        }
        public IActionResult Usuario()
        {
            return View();
        }
        public async Task<IActionResult> RegistroUsuario([Bind("Id,Correo,Contrasena,IMGarchivo,ReservaExitosa,Idpersona,Idrol")] Usuario usuario)
        {         
            try
            {
                if (usuario.Correo!="" & usuario.Contrasena !="" && usuario.Imagen !="")
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(usuario.IMGarchivo.FileName);
                    string extension = Path.GetExtension(usuario.IMGarchivo.FileName);
                    usuario.Imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imagenes", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await usuario.IMGarchivo.CopyToAsync(fileStream);
                    }
                    
                    
                    var usuar = await _context.Usuarios.AddAsync(usuario);
                    await _context.SaveChangesAsync();
                    GlobalVariables.Agregar(usuar.Entity.Correo);
                    GlobalVariables.EliminarPosiblePersona();
                    GlobalVariables.AgregarUsuario(usuar.Entity);
                    Console.WriteLine(GlobalVariables.StateGlobal);
                    Console.WriteLine(GlobalVariables.usuario);
                    return RedirectToAction("HomePage", "Home");
                }
                return RedirectToAction("Usuario", "Registro");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Usuario", "Registro");
            }
        }
    }
}
