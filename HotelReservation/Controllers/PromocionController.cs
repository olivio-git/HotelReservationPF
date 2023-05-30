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
    public class PromocionController : Controller
    {
        private readonly AppDbContext _context;

        public PromocionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
              return _context.Promociones != null ? 
                          View(await _context.Promociones.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Promociones'  is null.");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promociones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promocion == null)
            {
                return NotFound();
            }

            return View(promocion);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion")] Promocion promocion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promocion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promocion);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promociones.FindAsync(id);
            if (promocion == null)
            {
                return NotFound();
            }
            return View(promocion);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion")] Promocion promocion)
        {
            if (id != promocion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promocion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocionExists(promocion.Id))
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
            return View(promocion);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promociones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promocion == null)
            {
                return NotFound();
            }

            return View(promocion);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Promociones == null)
            {
                return Problem("Entity set 'AppDbContext.Promociones'  is null.");
            }
            var promocion = await _context.Promociones.FindAsync(id);
            if (promocion != null)
            {
                _context.Promociones.Remove(promocion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocionExists(int id)
        {
          return (_context.Promociones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
