using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPX.Data;
using SPX.Models;

namespace SPX.Controllers
{
    [Authorize]
    public class BucketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BucketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Buckets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Buckets.ToListAsync());
        }

        // GET: Buckets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bucket = await _context.Buckets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bucket == null)
            {
                return NotFound();
            }

            return View(bucket);
        }

        // GET: Buckets/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buckets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Bucket bucket)
        {
            if (ModelState.IsValid)
            {
                bucket.Id = Guid.NewGuid();
                _context.Add(bucket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bucket);
        }

        // GET: Buckets/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bucket = await _context.Buckets.FindAsync(id);
            if (bucket == null)
            {
                return NotFound();
            }
            return View(bucket);
        }

        // POST: Buckets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] Bucket bucket)
        {
            if (id != bucket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bucket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BucketExists(bucket.Id))
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
            return View(bucket);
        }

        // GET: Buckets/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bucket = await _context.Buckets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bucket == null)
            {
                return NotFound();
            }

            return View(bucket);
        }

        // POST: Buckets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bucket = await _context.Buckets.FindAsync(id);
            _context.Buckets.Remove(bucket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BucketExists(Guid id)
        {
            return _context.Buckets.Any(e => e.Id == id);
        }
    }
}
