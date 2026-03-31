using AstraTech.Routeplanner.Models;
using AstraTech.Routeplanner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AstraTech.Routeplanner.Controllers
{
    public class TransportRoutesController : Controller
    {
        private readonly AppDbContext _context;

        public TransportRoutesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var routes = await _context.TransportRoutes
                .Include(r => r.StartStation)
                .Include(r => r.DestinationStation)
                .ToListAsync();

            return View(routes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var route = await _context.TransportRoutes
                .Include(r => r.StartStation)
                .Include(r => r.DestinationStation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (route == null)
                return NotFound();

            return View(route);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransportRoute route)
        {
            if (route.StartStationId == route.DestinationStationId)
            {
                ModelState.AddModelError(string.Empty, "Start- und Zielstation dürfen nicht identisch sein.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(route);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var route = await _context.TransportRoutes.FindAsync(id);

            if (route == null)
                return NotFound();

            LoadDropdowns();
            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TransportRoute route)
        {
            if (id != route.Id)
                return NotFound();

            if (route.StartStationId == route.DestinationStationId)
            {
                ModelState.AddModelError(string.Empty, "Start- und Zielstation dürfen nicht identisch sein.");
            }

            if (ModelState.IsValid)
            {
                _context.Update(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(route);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var route = await _context.TransportRoutes
                .Include(r => r.StartStation)
                .Include(r => r.DestinationStation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (route == null)
                return NotFound();

            return View(route);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.TransportRoutes.FindAsync(id);

            if (route != null)
            {
                _context.TransportRoutes.Remove(route);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadDropdowns()
        {
            var stations = _context.Stations.ToList();

            ViewBag.Stations = new SelectList(stations, "Id", "Name");
            ViewBag.Priorities = new SelectList(new[] { "Niedrig", "Mittel", "Hoch", "Kritisch" });
            ViewBag.Statuses = new SelectList(new[] { "Geplant", "Freigegeben", "In Ausführung", "Abgeschlossen", "Storniert" });
        }
    }
}