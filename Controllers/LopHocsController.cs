using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LopHocsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LopHocsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LopHocs
        public async Task<IActionResult> Index()
        {
              return View(await _context.LopHoc.ToListAsync());
        }

        // GET: LopHocs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.LopHoc == null)
            {
                return NotFound();
            }

            var lopHoc = await _context.LopHoc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lopHoc == null)
            {
                return NotFound();
            }

            return View(lopHoc);
        }

        // GET: LopHocs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LopHocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaLopHoc,TenLop")] LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                lopHoc.Id = Guid.NewGuid();
                _context.Add(lopHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lopHoc);
        }

        // GET: LopHocs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.LopHoc == null)
            {
                return NotFound();
            }

            var lopHoc = await _context.LopHoc.FindAsync(id);
            if (lopHoc == null)
            {
                return NotFound();
            }
            return View(lopHoc);
        }

        // POST: LopHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,MaLopHoc,TenLop")] LopHoc lopHoc)
        {
            if (id != lopHoc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lopHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LopHocExists(lopHoc.Id))
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
            return View(lopHoc);
        }

        // GET: LopHocs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.LopHoc == null)
            {
                return NotFound();
            }

            var lopHoc = await _context.LopHoc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lopHoc == null)
            {
                return NotFound();
            }

            return View(lopHoc);
        }

        // POST: LopHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.LopHoc == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LopHoc'  is null.");
            }
            var lopHoc = await _context.LopHoc.FindAsync(id);
            if (lopHoc != null)
            {
                _context.LopHoc.Remove(lopHoc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LopHocExists(Guid id)
        {
          return _context.LopHoc.Any(e => e.Id == id);
        }
    }
}
