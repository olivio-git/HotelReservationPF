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
    public class PromocionReservaController : Controller
    {
        private readonly AppDbContext _context;

        public PromocionReservaController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PromocionReservas.Include(p => p.Promocion).Include(p => p.Reserva);
            return View(await appDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PromocionReservas == null)
            {
                return NotFound();
            }

            var promocionReserva = await _context.PromocionReservas
                .Include(p => p.Promocion)
                .Include(p => p.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promocionReserva == null)
            {
                return NotFound();
            }

            return View(promocionReserva);
        }
        public IActionResult Create()
        {
            ViewData["Idpromocion"] = new SelectList(_context.Promociones, "Id", "Titulo");
            ViewData["Idreserva"] = new SelectList(_context.Reservas, "Id", "FechaIn");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idpromocion,Idreserva")] PromocionReserva promocionReserva)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(promocionReserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["Idpromocion"] = new SelectList(_context.Promociones, "Id", "Titulo", promocionReserva.Idpromocion);
            //ViewData["Idreserva"] = new SelectList(_context.Reservas, "Id", "Id", promocionReserva.Idreserva);
            //return View(promocionReserva);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PromocionReservas == null)
            {
                return NotFound();
            }

            var promocionReserva = await _context.PromocionReservas.FindAsync(id);
            if (promocionReserva == null)
            {
                return NotFound();
            }
            ViewData["Idpromocion"] = new SelectList(_context.Promociones, "Id", "Titulo", promocionReserva.Idpromocion);
            ViewData["Idreserva"] = new SelectList(_context.Reservas, "Id", "FechaIn", promocionReserva.Reserva);
            return View(promocionReserva);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Idpromocion,Idreserva")] PromocionReserva promocionReserva)
        {
            if (id != promocionReserva.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(promocionReserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocionReservaExists(promocionReserva.Id))
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
            //ViewData["Idpromocion"] = new SelectList(_context.Promociones, "Id", "Titulo", promocionReserva.Idpromocion);
            //ViewData["Idreserva"] = new SelectList(_context.Reservas, "Id", "Id", promocionReserva.Idreserva);
            //return View(promocionReserva);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PromocionReservas == null)
            {
                return NotFound();
            }

            var promocionReserva = await _context.PromocionReservas
                .Include(p => p.Promocion)
                .Include(p => p.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promocionReserva == null)
            {
                return NotFound();
            }

            return View(promocionReserva);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PromocionReservas == null)
            {
                return Problem("Entity set 'AppDbContext.PromocionReservas'  is null.");
            }
            var promocionReserva = await _context.PromocionReservas.FindAsync(id);
            if (promocionReserva != null)
            {
                _context.PromocionReservas.Remove(promocionReserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocionReservaExists(int id)
        {
          return (_context.PromocionReservas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
