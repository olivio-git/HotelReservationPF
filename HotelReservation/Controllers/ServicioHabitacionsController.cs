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
    public class ServicioHabitacionsController : Controller
    {
        private readonly AppDbContext _context;

        public ServicioHabitacionsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ServicioHabitaciones.Include(s => s.Servicio).Include(s => s.tipo);
            return View(await appDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServicioHabitaciones == null)
            {
                return NotFound();
            }

            var servicioHabitacion = await _context.ServicioHabitaciones
                .Include(s => s.Servicio)
                .Include(s => s.tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicioHabitacion == null)
            {
                return NotFound();
            }

            return View(servicioHabitacion);
        }
        public IActionResult Create()
        {
            ViewData["Idservicio"] = new SelectList(_context.Servicios, "Id", "Nombre");
            ViewData["Idtipo"] = new SelectList(_context.Tipos, "Id", "Nombre");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado,Idservicio,Idtipo")] ServicioHabitacion servicioHabitacion)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(servicioHabitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["Idservicio"] = new SelectList(_context.Servicios, "Id", "Nombre", servicioHabitacion.Idservicio);
            //ViewData["Idtipo"] = new SelectList(_context.Tipos, "Id", "Nombre", servicioHabitacion.Idtipo);
            //return View(servicioHabitacion);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServicioHabitaciones == null)
            {
                return NotFound();
            }

            var servicioHabitacion = await _context.ServicioHabitaciones.FindAsync(id);
            if (servicioHabitacion == null)
            {
                return NotFound();
            }
            ViewData["Idservicio"] = new SelectList(_context.Servicios, "Id", "Nombre", servicioHabitacion.Idservicio);
            ViewData["Idtipo"] = new SelectList(_context.Tipos, "Id", "Nombre", servicioHabitacion.Idtipo);
            return View(servicioHabitacion);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estado,Idservicio,Idtipo")] ServicioHabitacion servicioHabitacion)
        {
            if (id != servicioHabitacion.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(servicioHabitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioHabitacionExists(servicioHabitacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["Idservicio"] = new SelectList(_context.Servicios, "Id", "Nombre", servicioHabitacion.Idservicio);
            //ViewData["Idtipo"] = new SelectList(_context.Tipos, "Id", "Nombre", servicioHabitacion.Idtipo);
            //return View(servicioHabitacion);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServicioHabitaciones == null)
            {
                return NotFound();
            }

            var servicioHabitacion = await _context.ServicioHabitaciones
                .Include(s => s.Servicio)
                .Include(s => s.tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicioHabitacion == null)
            {
                return NotFound();
            }

            return View(servicioHabitacion);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServicioHabitaciones == null)
            {
                return Problem("Entity set 'AppDbContext.ServicioHabitaciones'  is null.");
            }
            var servicioHabitacion = await _context.ServicioHabitaciones.FindAsync(id);
            if (servicioHabitacion != null)
            {
                _context.ServicioHabitaciones.Remove(servicioHabitacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioHabitacionExists(int id)
        {
          return (_context.ServicioHabitaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
