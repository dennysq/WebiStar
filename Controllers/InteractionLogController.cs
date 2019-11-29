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
    public class InteractionLogController : Controller
    {
        private readonly EscuelaContext _context;

        public InteractionLogController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: InteractionLog
        public async Task<IActionResult> Index()
        {
            return View(await _context.InteractionLog.ToListAsync());
        }

        // GET: InteractionLog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interactionLog = await _context.InteractionLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interactionLog == null)
            {
                return NotFound();
            }

            return View(interactionLog);
        }

        // GET: InteractionLog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InteractionLog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Event,Timestamp,Description,WebinarMeetingId,Id,Modified")] InteractionLog interactionLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interactionLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(interactionLog);
        }

        // GET: InteractionLog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interactionLog = await _context.InteractionLog.FindAsync(id);
            if (interactionLog == null)
            {
                return NotFound();
            }
            return View(interactionLog);
        }

        // POST: InteractionLog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Event,Timestamp,Description,WebinarMeetingId,Id,Modified")] InteractionLog interactionLog)
        {
            if (id != interactionLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interactionLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InteractionLogExists(interactionLog.Id))
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
            return View(interactionLog);
        }

        // GET: InteractionLog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interactionLog = await _context.InteractionLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interactionLog == null)
            {
                return NotFound();
            }

            return View(interactionLog);
        }

        // POST: InteractionLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interactionLog = await _context.InteractionLog.FindAsync(id);
            _context.InteractionLog.Remove(interactionLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InteractionLogExists(int id)
        {
            return _context.InteractionLog.Any(e => e.Id == id);
        }
    }
}
