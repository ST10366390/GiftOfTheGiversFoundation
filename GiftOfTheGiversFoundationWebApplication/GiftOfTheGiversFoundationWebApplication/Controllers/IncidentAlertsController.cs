using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGiversFoundationWebApplication.Data;
using GiftOfTheGiversFoundationWebApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace GiftOfTheGiversFoundationWebApplication.Controllers
{
    public class IncidentAlertsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IncidentAlertsController(AppDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: IncidentAlerts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.IncidentAlerts.Include(i => i.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: IncidentAlerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidentAlert = await _context.IncidentAlerts
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.IncidentAlertId == id);
            if (incidentAlert == null)
            {
                return NotFound();
            }

            return View(incidentAlert);
        }

        // GET: IncidentAlerts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: IncidentAlerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IncidentAlertId,Title,Description,Date,Location")] IncidentAlert incidentAlert)
        {
            var user = await _userManager.GetUserAsync(User);
            var appUser = _context.Users.FirstOrDefault(t => t.EmailAddress == user.Email);
            incidentAlert.UserId = appUser.UserId;
            incidentAlert.User = appUser;

            _context.Add(incidentAlert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: IncidentAlerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidentAlert = await _context.IncidentAlerts.FindAsync(id);
            if (incidentAlert == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", incidentAlert.UserId);
            return View(incidentAlert);
        }

        // POST: IncidentAlerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IncidentAlertId,UserId,Title,Description,Date,Location")] IncidentAlert incidentAlert)
        {
            if (id != incidentAlert.IncidentAlertId)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var appUser = _context.Users.FirstOrDefault(t => t.EmailAddress == user.Email);
            incidentAlert.UserId = appUser.UserId;
            incidentAlert.User = appUser;

            try
                {
                    _context.Update(incidentAlert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentAlertExists(incidentAlert.IncidentAlertId))
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

        // GET: IncidentAlerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidentAlert = await _context.IncidentAlerts
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.IncidentAlertId == id);
            if (incidentAlert == null)
            {
                return NotFound();
            }

            return View(incidentAlert);
        }

        // POST: IncidentAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incidentAlert = await _context.IncidentAlerts.FindAsync(id);
            if (incidentAlert != null)
            {
                _context.IncidentAlerts.Remove(incidentAlert);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidentAlertExists(int id)
        {
            return _context.IncidentAlerts.Any(e => e.IncidentAlertId == id);
        }
    }
}
