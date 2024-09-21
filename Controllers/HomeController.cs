using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Verzamelwoede.Data;
using Verzamelwoede.Models;

namespace Verzamelwoede.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VerzamelwoedeDB _context;

        public HomeController(ILogger<HomeController> logger, VerzamelwoedeDB context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: /Home/Items
        public async Task<IActionResult> Items()
        {
            var items = await _context.Items.ToListAsync();
            return View(items);
        }

        // GET: /Home/Item/5
        public async Task<IActionResult> Item(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
