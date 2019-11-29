using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp_net_core;
using asp_net_core.Models;

namespace asp_net_core.Controllers
{
    public class MTimeZoneController : Controller
    {
        private readonly EscuelaContext _context;

        public MTimeZoneController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: MTimeZone
        public async Task<IActionResult> Index()
        {
            return View(await _context.MTimeZone.ToListAsync());
        }

        // GET: MTimeZone/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mTimeZone = await _context.MTimeZone
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mTimeZone == null)
            {
                return NotFound();
            }

            return View(mTimeZone);
        }

        // GET: MTimeZone/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MTimeZone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,UtcOffset,Dst,Id,Modified")] MTimeZone mTimeZone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mTimeZone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mTimeZone);
        }

        // GET: MTimeZone/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mTimeZone = await _context.MTimeZone.FindAsync(id);
            if (mTimeZone == null)
            {
                return NotFound();
            }
            return View(mTimeZone);
        }

        // POST: MTimeZone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,UtcOffset,Dst,Id,Modified")] MTimeZone mTimeZone)
        {
            if (id != mTimeZone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mTimeZone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MTimeZoneExists(mTimeZone.Id))
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
            return View(mTimeZone);
        }

        // GET: MTimeZone/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mTimeZone = await _context.MTimeZone
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mTimeZone == null)
            {
                return NotFound();
            }

            return View(mTimeZone);
        }

        // POST: MTimeZone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mTimeZone = await _context.MTimeZone.FindAsync(id);
            _context.MTimeZone.Remove(mTimeZone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MTimeZoneExists(string id)
        {
            return _context.MTimeZone.Any(e => e.Id == id);
        }
    }
}
