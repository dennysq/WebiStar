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
    public class WebinarMeetingController : Controller
    {
        private readonly EscuelaContext _context;

        public WebinarMeetingController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: WebinarMeeting
        public async Task<IActionResult> Index()
        {
            var escuelaContext = _context.WebinarMeeting.Include(w => w.User);
            return View(await escuelaContext.ToListAsync());
        }

        // GET: WebinarMeeting/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webinarMeeting = await _context.WebinarMeeting
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webinarMeeting == null)
            {
                return NotFound();
            }

            return View(webinarMeeting);
        }

        // GET: WebinarMeeting/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName");
            return View();
        }

        // POST: WebinarMeeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartDate,Duration,Password,HostVideoEnabled,ParticipantVideoEnabled,MaxParticipants,Description,Name,BannerUrl,UserId,Price,Id,Modified")] WebinarMeeting webinarMeeting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webinarMeeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName", webinarMeeting.UserId);
            return View(webinarMeeting);
        }

        // GET: WebinarMeeting/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webinarMeeting = await _context.WebinarMeeting.FindAsync(id);
            if (webinarMeeting == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName", webinarMeeting.UserId);
            return View(webinarMeeting);
        }

        // POST: WebinarMeeting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartDate,Duration,Password,HostVideoEnabled,ParticipantVideoEnabled,MaxParticipants,Description,Name,BannerUrl,UserId,Price,Id,Modified")] WebinarMeeting webinarMeeting)
        {
            if (id != webinarMeeting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webinarMeeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebinarMeetingExists(webinarMeeting.Id))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName", webinarMeeting.UserId);
            return View(webinarMeeting);
        }

        // GET: WebinarMeeting/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webinarMeeting = await _context.WebinarMeeting
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webinarMeeting == null)
            {
                return NotFound();
            }

            return View(webinarMeeting);
        }

        // POST: WebinarMeeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webinarMeeting = await _context.WebinarMeeting.FindAsync(id);
            _context.WebinarMeeting.Remove(webinarMeeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebinarMeetingExists(int id)
        {
            return _context.WebinarMeeting.Any(e => e.Id == id);
        }
    }
}
