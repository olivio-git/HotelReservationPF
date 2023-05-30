using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservation;
using HotelReservation.Models;
using Microsoft.Extensions.Hosting;

namespace HotelReservation.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UsuarioController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Usuarios.Include(u => u.Persona).Include(u => u.Rol);
            return View(await appDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        public IActionResult Create()
        {
            ViewData["Idpersona"] = new SelectList(_context.Personas, "Id", "Apellido");
            ViewData["Idrol"] = new SelectList(_context.Roles, "Id", "Nombre");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Correo,Contrasena,Imagen,ReservaExitosa,Idpersona,Idrol,IMGarchivo")] Usuario usuario)
        {
            //AGREGAR IMAGEN
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(usuario.IMGarchivo.FileName);
            string extension = Path.GetExtension(usuario.IMGarchivo.FileName);
            usuario.Imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/imagenes", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await usuario.IMGarchivo.CopyToAsync(fileStream);
            }
            //--------------

            //if (ModelState.IsValid)
            //{
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["Idpersona"] = new SelectList(_context.Personas, "Id", "Apellido", usuario.Idpersona);
            //ViewData["Idrol"] = new SelectList(_context.Roles, "Id", "Nombre", usuario.Idrol);
            //return View(usuario);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["Idpersona"] = new SelectList(_context.Personas, "Id", "Apellido", usuario.Idpersona);
            ViewData["Idrol"] = new SelectList(_context.Roles, "Id", "Nombre", usuario.Idrol);
            return View(usuario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Correo,Contrasena,Imagen,ReservaExitosa,Idpersona,Idrol,IMGarchivo")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            try
            {
                // Obtener la entidad original de la base de datos
                var originausuario = await _context.Usuarios.FindAsync(id);
                if (originausuario == null)
                {
                    return NotFound();
                }
                if (usuario.IMGarchivo == null || usuario.IMGarchivo.Length == 0)
                {
                    usuario.Imagen = originausuario.Imagen;
                }
                else
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(usuario.IMGarchivo.FileName);
                    string extension = Path.GetExtension(usuario.IMGarchivo.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "imagenes", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await usuario.IMGarchivo.CopyToAsync(fileStream);
                    }

                    usuario.Imagen = fileName;
                }
                _context.Entry(originausuario).CurrentValues.SetValues(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'AppDbContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
