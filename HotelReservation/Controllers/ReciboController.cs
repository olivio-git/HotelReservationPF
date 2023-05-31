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
    public class ReciboController : Controller
    {
        private readonly AppDbContext _context;

        public ReciboController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Recibos.Include(r => r.Reserva);
            return View(await appDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recibos == null)
            {
                return NotFound();
            }

            var recibo = await _context.Recibos
                .Include(r => r.Reserva.Habitacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recibo == null)
            {
                return NotFound();
            }
            var reserba = _context.Recibos.Include(x => x.Reserva.Habitacion.tipo).Include(x=>x.Reserva.Usuario.Persona).FirstOrDefault(x=>x.Id == id);
            if (reserba != null)
            {
                ViewData["lista"] = _context.ServicioHabitaciones.Include(x => x.Servicio).Where(y => y.tipo.Id == reserba.Reserva.Habitacion.tipo.Id).Where(z => z.Estado == true);
                ViewData["habitacion"] = reserba; 
            }
            return View(recibo);
        }
        public IActionResult Create()
        {
            ViewData["Idreserva"] = new SelectList(_context.Reservas, "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Total,Idreserva")] Recibo recibo)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(recibo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["Idreserva"] = new SelectList(_context.Reservas, "Id", "Id", recibo.Idreserva);
            //return View(recibo);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recibos == null)
            {
                return NotFound();
            }

            var recibo = await _context.Recibos.FindAsync(id);
            if (recibo == null)
            {
                return NotFound();
            }
            ViewData["Idreserva"] = new SelectList(_context.Reservas, "Id", "Id", recibo.Idreserva);
            return View(recibo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Total,Idreserva")] Recibo recibo)
        {
            if (id != recibo.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(recibo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReciboExists(recibo.Id))
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
            //ViewData["Idreserva"] = new SelectList(_context.Reservas, "Id", "Id", recibo.Idreserva);
            //return View(recibo);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recibos == null)
            {
                return NotFound();
            }

            var recibo = await _context.Recibos
                .Include(r => r.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recibo == null)
            {
                return NotFound();
            }

            return View(recibo);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recibos == null)
            {
                return Problem("Entity set 'AppDbContext.Recibos'  is null.");
            }
            var recibo = await _context.Recibos.FindAsync(id);
            if (recibo != null)
            {
                _context.Recibos.Remove(recibo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReciboExists(int id)
        {
          return (_context.Recibos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
