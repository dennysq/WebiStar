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
    public class MediaRecordController : Controller
    {
        private readonly EscuelaContext _context;

        public MediaRecordController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: MediaRecord
        public async Task<IActionResult> Index()
        {
            return View(await _context.MediaRecord.ToListAsync());
        }

        // GET: MediaRecord/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaRecord = await _context.MediaRecord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaRecord == null)
            {
                return NotFound();
            }

            return View(mediaRecord);
        }

        // GET: MediaRecord/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MediaRecord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Data,WebinarMeetingId,ExpireDate,Id,Modified")] MediaRecord mediaRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediaRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediaRecord);
        }

        // GET: MediaRecord/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaRecord = await _context.MediaRecord.FindAsync(id);
            if (mediaRecord == null)
            {
                return NotFound();
            }
            return View(mediaRecord);
        }

        // POST: MediaRecord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Data,WebinarMeetingId,ExpireDate,Id,Modified")] MediaRecord mediaRecord)
        {
            if (id != mediaRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaRecordExists(mediaRecord.Id))
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
            return View(mediaRecord);
        }

        // GET: MediaRecord/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaRecord = await _context.MediaRecord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaRecord == null)
            {
                return NotFound();
            }

            return View(mediaRecord);
        }

        // POST: MediaRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mediaRecord = await _context.MediaRecord.FindAsync(id);
            _context.MediaRecord.Remove(mediaRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaRecordExists(string id)
        {
            return _context.MediaRecord.Any(e => e.Id == id);
        }
    }
}
