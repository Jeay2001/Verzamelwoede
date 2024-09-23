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

        public async Task<IActionResult> Index()
        {
            var items = await _context.Items.ToListAsync(); // Fetch all items from the database
            var collections = await _context.Collections.ToListAsync(); // Fetch all collections from the database

            var viewModel = new HomePageViewModel
            {
                Items = items,
                Collections = collections
            };

            return View(viewModel); // Pass the ViewModel to the view
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
