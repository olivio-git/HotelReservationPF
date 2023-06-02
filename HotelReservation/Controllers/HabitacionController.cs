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
    public class HabitacionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HabitacionController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Habitaciones.Include(h => h.tipo);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Habitaciones == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitaciones
                .Include(h => h.tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habitacion == null)
            {
                return NotFound();
            }
            var data = await _context.Habitaciones.Include(x => x.tipo).FirstOrDefaultAsync(y=>y.Id == id);
            if (data != null)
            {
                ViewData["lista"] = _context.ServicioHabitaciones.Include(x => x.Servicio).Where(y=>y.tipo.Id == data.tipo.Id).Where(z=>z.Estado == true);
            }
            return View(habitacion);
        }

        public IActionResult Create()
        {
            ViewData["Idtipo"] = new SelectList(_context.Tipos, "Id", "Nombre");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Imagen,Estado,Calificacion,Idtipo,IMGarchivo")] Habitacion habitacion)
        {
            //AGREGAR IMAGEN
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(habitacion.IMGarchivo.FileName);
            string extension = Path.GetExtension(habitacion.IMGarchivo.FileName);
            habitacion.Imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/imagenes", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await habitacion.IMGarchivo.CopyToAsync(fileStream);
            }
            //--------------


            //if (ModelState.IsValid)
            //{
            _context.Add(habitacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["Idtipo"] = new SelectList(_context.Tipos, "Id", "Nombre", habitacion.Idtipo);
            //return View(habitacion);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Habitaciones == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            ViewData["Idtipo"] = new SelectList(_context.Tipos, "Id", "Nombre", habitacion.Idtipo);
            return View(habitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Imagen,Estado,Calificacion,Idtipo,IMGarchivo")] Habitacion habitacion)
        {
            if (id != habitacion.Id)
            {
                return NotFound();
            }

            try
            {
                // Obtener la entidad original de la base de datos
                var originalHabitacion = await _context.Habitaciones.FindAsync(id);
                if (originalHabitacion == null)
                {
                    return NotFound();
                }
                if (habitacion.IMGarchivo == null || habitacion.IMGarchivo.Length == 0)
                {
                    habitacion.Imagen = originalHabitacion.Imagen;
                }
                else
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(habitacion.IMGarchivo.FileName);
                    string extension = Path.GetExtension(habitacion.IMGarchivo.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "imagenes", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await habitacion.IMGarchivo.CopyToAsync(fileStream);
                    }

                    habitacion.Imagen = fileName;
                }
                _context.Entry(originalHabitacion).CurrentValues.SetValues(habitacion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitacionExists(habitacion.Id))
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Imagen,Estado,Calificacion,Idtipo,IMGarchivo")] Habitacion habitacion)
        //{
        //    if (id != habitacion.Id)
        //    {
        //        return NotFound();
        //    }

        //    //if (ModelState.IsValid)
        //    //{
        //    try
        //    {
        //        string wwwRootPath = _hostEnvironment.WebRootPath;
        //        string? fileName = Path.GetFileNameWithoutExtension(habitacion.IMGarchivo.FileName);
        //        string extension = Path.GetExtension(habitacion.IMGarchivo.FileName);
        //        habitacion.Imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //        string path = Path.Combine(wwwRootPath + "/imagenes", fileName);
        //        using (var fileStream = new FileStream(path, FileMode.Create))
        //        {
        //            await habitacion.IMGarchivo.CopyToAsync(fileStream);
        //        }

        //        habitacion.Imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

        //        _context.Update(habitacion);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HabitacionExists(habitacion.Id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //    //}
        //    //ViewData["Idtipo"] = new SelectList(_context.Tipos, "Id", "Nombre", habitacion.Idtipo);
        //    //return View(habitacion);
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Habitaciones == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitaciones
                .Include(h => h.tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Habitaciones == null)
            {
                return Problem("Entity set 'AppDbContext.Habitaciones'  is null.");
            }
            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion != null)
            {
                _context.Habitaciones.Remove(habitacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitacionExists(int id)
        {
            return (_context.Habitaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> ViewHabitations()
        {
            var appDbContext = _context.Habitaciones.Include(h => h.tipo);
            return View(await appDbContext.ToListAsync());
        }
    }
}
