﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VerzamelWoede.Models;
using Verzamelwoede.Data;
using Microsoft.Data.SqlClient;
using Verzamelwoede.Models;

namespace Verzamelwoede.Controllers
{
    public class ItemsController : Controller
    {
        private readonly VerzamelwoedeDB _context;

        public ItemsController(VerzamelwoedeDB context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(string sortOrder, string searchTerm)
        {
            // Get all items including the category
            var items = from i in _context.Items.Include(i => i.Category)
                        select i;

            // Handle searching
            if (!String.IsNullOrEmpty(searchTerm))
            {
                items = items.Where(i => i.Name.Contains(searchTerm));
            }

            // Handle sorting
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(i => i.Name);
                    break;
                case "price_asc":
                    items = items.OrderBy(i => i.Price);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(i => i.Price);
                    break;
                case "date_asc":
                    items = items.OrderBy(i => i.YearOfPurchase);
                    break;
                case "date_desc":
                    items = items.OrderByDescending(i => i.YearOfPurchase);
                    break;
                case "category_asc":
                    items = items.OrderBy(i => i.Category.Name);
                    break;
                case "category_desc":
                    items = items.OrderByDescending(i => i.Category.Name);
                    break;
                default:
                    items = items.OrderBy(i => i.Name); // Default to sorting by name ascending
                    break;
            }

            // Convert each Item into ItemViewModel
            var viewModelList = await items.Select(i => new ItemViewModel
            {
                Item = i
            }).ToListAsync();

            // Calculate the grand total
            decimal grandTotalPrice = viewModelList.Sum(vm => vm.TotalPrice);

            // Pass the search term back to the view
            ViewData["SearchTerm"] = searchTerm;

            // Pass the grand total to the view
            ViewBag.GrandTotalPrice = grandTotalPrice;

            return View(viewModelList);
        }


        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Quantity,Price,Worth,YearOfPurchase,Condition,Origin,IsSold,IsLost,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Log the model state errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Quantity,Price,Worth,YearOfPurchase,Condition,Origin,IsSold,IsLost,CategoryId")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
