using System.Runtime.CompilerServices;
using AstraTech.Routeplanner.Services;
using AstraTech.Routeplanner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AstraTech.Routeplanner.Controllers
{
    public class NavigationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly NavigationService _navigationService;

        public NavigationController(AppDbContext context, NavigationService navigationService)
        {
            _context = context;
            _navigationService = navigationService;
        }

        public IActionResult Index()
        {
            LoadStations();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int fromStationId, int toStationId)
        {
            LoadStations();

            if(fromStationId == toStationId)
            {
                ModelState.AddModelError(string.Empty, "Start- und Zielstation dürfen nicht identisch sein.");
                return View();
            }

            var stations = await _context.Stations.ToListAsync();
            var connections = await _context.Connections.ToListAsync();

            var result = _navigationService.CalculateShortestPath(
                stations,
                connections,
                fromStationId,
                toStationId);

            return View(result);
        }

        private void LoadStations()
        {
            var stations = _context.Stations.ToList();
            ViewBag.Stations = new SelectList(stations, "Id", "Name");
        }
    }
}
