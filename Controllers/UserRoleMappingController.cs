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
    public class UserRoleMappingController : Controller
    {
        private readonly EscuelaContext _context;

        public UserRoleMappingController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: UserRoleMapping
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserRoleMapping.ToListAsync());
        }

        // GET: UserRoleMapping/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleMapping = await _context.UserRoleMapping
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRoleMapping == null)
            {
                return NotFound();
            }

            return View(userRoleMapping);
        }

        // GET: UserRoleMapping/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserRoleMapping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,UserId,Id,Modified")] UserRoleMapping userRoleMapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRoleMapping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userRoleMapping);
        }

        // GET: UserRoleMapping/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleMapping = await _context.UserRoleMapping.FindAsync(id);
            if (userRoleMapping == null)
            {
                return NotFound();
            }
            return View(userRoleMapping);
        }

        // POST: UserRoleMapping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,UserId,Id,Modified")] UserRoleMapping userRoleMapping)
        {
            if (id != userRoleMapping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRoleMapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoleMappingExists(userRoleMapping.Id))
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
            return View(userRoleMapping);
        }

        // GET: UserRoleMapping/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleMapping = await _context.UserRoleMapping
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRoleMapping == null)
            {
                return NotFound();
            }

            return View(userRoleMapping);
        }

        // POST: UserRoleMapping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRoleMapping = await _context.UserRoleMapping.FindAsync(id);
            _context.UserRoleMapping.Remove(userRoleMapping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoleMappingExists(int id)
        {
            return _context.UserRoleMapping.Any(e => e.Id == id);
        }
    }
}
