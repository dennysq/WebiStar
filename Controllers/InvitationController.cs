using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp_net_core;
using asp_net_core.Models;
using SendGrid;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;

namespace asp_net_core.Controllers
{
    public class InvitationController : Controller
    {
        private readonly EscuelaContext _context;

        public InvitationController(EscuelaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private readonly IConfiguration _configuration;

        // GET: Invitation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Invitation.ToListAsync());
        }

        // GET: Invitation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invitation = await _context.Invitation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitation == null)
            {
                return NotFound();
            }

            return View(invitation);
        }

        // GET: Invitation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Invitation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,EmailAddress,Subject,Message,Accepted,WebinarMeetingId,Id,Modified")] Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invitation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invitation);
        }

        // GET: Invitation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invitation = await _context.Invitation.FindAsync(id);
            if (invitation == null)
            {
                return NotFound();
            }
            return View(invitation);
        }

        // POST: Invitation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,EmailAddress,Subject,Message,Accepted,WebinarMeetingId,Id,Modified")] Invitation invitation)
        {
            if (id != invitation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invitation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvitationExists(invitation.Id))
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
            return View(invitation);
        }

        // GET: Invitation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invitation = await _context.Invitation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitation == null)
            {
                return NotFound();
            }

            return View(invitation);
        }

        // POST: Invitation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invitation = await _context.Invitation.FindAsync(id);
            _context.Invitation.Remove(invitation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvitationExists(int id)
        {
            return _context.Invitation.Any(e => e.Id == id);
        }
        private void SendInvitation()
        {


        }
        // [Route("SendNotification")]
        // IActionResult
      


    }
}
