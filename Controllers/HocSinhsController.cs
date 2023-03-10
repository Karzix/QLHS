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
    public class HocSinhsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HocSinhsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HocSinhs
        public async Task<IActionResult> Index(string? TenHS,Guid? LopHocId, string sort , string sortdes)
        {
            var applicationDbContext = _context.HocSinh.Include(h => h.LopHoc).AsQueryable();
            //tim kiem
            if (!string.IsNullOrEmpty(TenHS))
            {
                applicationDbContext = applicationDbContext.Where(hs=>hs.HoVaTen.Contains(TenHS));
                ViewBag.TenHS = TenHS;
            }
            if (LopHocId != null)
            {
                applicationDbContext = applicationDbContext.Where(lh=>lh.LopHocId==LopHocId);
                ViewBag.LopHocId= LopHocId;
            }
            ViewData["LopHocId"] = new SelectList(_context.LopHoc, "Id", "TenLop");
            //sap xep
            ViewBag.sortMaSV = String.IsNullOrEmpty(sort) ? "MaSV" : "";

            if(sort != null)
            {
                applicationDbContext = applicationDbContext.OrderBy(hs => hs.MaSV);
            }
            if (sortdes != null)
            {
                applicationDbContext = applicationDbContext.OrderByDescending(hs => hs.MaSV);
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HocSinhs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.HocSinh == null)
            {
                return NotFound();
            }

            var hocSinh = await _context.HocSinh
                .Include(h => h.LopHoc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hocSinh == null)
            {
                return NotFound();
            }

            return View(hocSinh);
        }

        // GET: HocSinhs/Create
        public IActionResult Create()
        {
            ViewData["LopHocId"] = new SelectList(_context.LopHoc, "Id", "TenLop");
            return View();
        }

        // POST: HocSinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaSV,HoVaTen,LopHocId")] HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                hocSinh.Id = Guid.NewGuid();
                _context.Add(hocSinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LopHocId"] = new SelectList(_context.LopHoc, "Id", "TenLop", hocSinh.LopHocId);
            return View(hocSinh);
        }

        // GET: HocSinhs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.HocSinh == null)
            {
                return NotFound();
            }

            var hocSinh = await _context.HocSinh.FindAsync(id);
            if (hocSinh == null)
            {
                return NotFound();
            }
            ViewData["LopHocId"] = new SelectList(_context.LopHoc, "Id", "TenLop", hocSinh.LopHocId);
            return View(hocSinh);
        }
        // POST: HocSinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,MaSV,HoVaTen,LopHocId")] HocSinh hocSinh)
        {
            if (id != hocSinh.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocSinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocSinhExists(hocSinh.Id))
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
            ViewData["LopHocId"] = new SelectList(_context.LopHoc, "Id", "TenLop", hocSinh.LopHocId);
            return View(hocSinh);
        }

        // GET: HocSinhs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.HocSinh == null)
            {
                return NotFound();
            }

            var hocSinh = await _context.HocSinh
                .Include(h => h.LopHoc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hocSinh == null)
            {
                return NotFound();
            }

            return View(hocSinh);
        }

        // POST: HocSinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.HocSinh == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HocSinh'  is null.");
            }
            var hocSinh = await _context.HocSinh.FindAsync(id);
            if (hocSinh != null)
            {
                _context.HocSinh.Remove(hocSinh);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocSinhExists(Guid id)
        {
          return _context.HocSinh.Any(e => e.Id == id);
        }
    }
}
