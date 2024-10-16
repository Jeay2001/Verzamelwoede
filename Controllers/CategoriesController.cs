﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VerzamelWoede.Models;
using Verzamelwoede.Data;
using Verzamelwoede.Models;

namespace Verzamelwoede.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly VerzamelwoedeDB _context;

        public CategoriesController(VerzamelwoedeDB context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var viewModel = new CategoryViewModel
            {
                Categories = await _context.Categories.Include(c => c.Collections).ToListAsync(),
                Collections = await _context.Collections.ToListAsync()
            };
            return View(viewModel);
        }


        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
            .Include(c => c.Items)  // Ensure we load the items related to the category
            .Include(c => c.Collections) // If needed, still include collections
            .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel
            {
                Category = category,
                Items = category.Items.ToList(), // Get the items for this category
                Categories = new List<Category> { category },  // Pass the selected Category
                Collections = category.Collections.ToList()    // Pass the related Collections
            };

            return View(viewModel);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            var viewModel = new CategoryViewModel
            {
                Collections = _context.Collections.ToList() // Populate the available collections
            };
            return View(viewModel);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = viewModel.Category.Name, // Get the category name from the view model
                    Collections = _context.Collections
                                    .Where(c => viewModel.SelectedCollectionIds.Contains(c.Id)) // Attach selected collections
                                    .ToList()
                };

                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Collections = _context.Collections.ToList(); // If model state is invalid, repopulate collections
            return View(viewModel);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Collections)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            // Initialize the view model and handle empty collections
            var viewModel = new CategoryViewModel
            {
                Category = category,
                Categories = await _context.Categories.ToListAsync(),
                Collections = await _context.Collections.ToListAsync(),
                SelectedCollectionIds = category.Collections?.Select(c => c.Id).ToList() ?? new List<int>()
            };

            return View(viewModel);
        }


        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel viewModel)
        {
            // Check if the ID in the view model matches the ID from the route
            if (id != viewModel.Category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    // Find the existing category
                    var category = await _context.Categories
                        .Include(c => c.Collections)
                        .FirstOrDefaultAsync(c => c.Id == id);

                    if (category == null)
                    {
                        return NotFound(); // Return NotFound if category doesn't exist
                    }

                    // Update the category's collections based on the selected IDs
                    category.Collections = await _context.Collections
                        .Where(c => viewModel.SelectedCollectionIds.Contains(c.Id))
                        .ToListAsync();

                    // Update the category name
                    category.Name = viewModel.Category.Name;

                    // Mark the category as modified
                    _context.Entry(category).State = EntityState.Modified;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index)); // Redirect to the Index action on success
            }

            // If ModelState is invalid or there are errors, reload collections for the view model
            viewModel.Collections = await _context.Collections.ToListAsync();
            return View(viewModel);
        }


        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Collections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
