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
    public class ReliefEffortsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReliefEffortsController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ReliefEfforts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReliefEfforts.Include(r => r.IncidentAlert).Include(r => r.Volunteer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReliefEfforts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reliefEffort = await _context.ReliefEfforts
                .Include(r => r.IncidentAlert)
                .Include(r => r.Volunteer)
                .FirstOrDefaultAsync(m => m.ReliefEffortId == id);
            if (reliefEffort == null)
            {
                return NotFound();
            }

            return View(reliefEffort);
        }

        // GET: ReliefEfforts/Create
        public IActionResult Create()
        {
            ViewData["IncidentAlertId"] = new SelectList(_context.IncidentAlerts, "IncidentAlertId", "IncidentAlertId");
            ViewData["VolunteerId"] = new SelectList(_context.Volunteers, "VolunteerId", "VolunteerId");
            return View();
        }

        // POST: ReliefEfforts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReliefEffortId,IncidentAlertId,VolunteerId,Title,Description,Date,Location")] ReliefEffort reliefEffort)
        {
            var user = await _userManager.GetUserAsync(User);
            GiftOfTheGiversFoundationWebApplication.Models.Volunteer volunteer = _context.Volunteers.FirstOrDefault(u => u.VolunteerId == reliefEffort.VolunteerId);
            GiftOfTheGiversFoundationWebApplication.Models.IncidentAlert incident = _context.IncidentAlerts.FirstOrDefault(u => u.IncidentAlertId == reliefEffort.IncidentAlertId);

            reliefEffort.Volunteer = volunteer;
            reliefEffort.IncidentAlert = incident;

            
            _context.Add(reliefEffort);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: ReliefEfforts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reliefEffort = await _context.ReliefEfforts.FindAsync(id);
            if (reliefEffort == null)
            {
                return NotFound();
            }
            ViewData["IncidentAlertId"] = new SelectList(_context.IncidentAlerts, "IncidentAlertId", "IncidentAlertId", reliefEffort.IncidentAlertId);
            ViewData["VolunteerId"] = new SelectList(_context.Volunteers, "VolunteerId", "VolunteerId", reliefEffort.VolunteerId);
            return View(reliefEffort);
        }

        // POST: ReliefEfforts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReliefEffortId,IncidentAlertId,VolunteerId,Title,Description,Date,Location")] ReliefEffort reliefEffort)
        {
            if (id != reliefEffort.ReliefEffortId)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            GiftOfTheGiversFoundationWebApplication.Models.Volunteer volunteer = _context.Volunteers.FirstOrDefault(u => u.VolunteerId == reliefEffort.VolunteerId);
            GiftOfTheGiversFoundationWebApplication.Models.IncidentAlert incident = _context.IncidentAlerts.FirstOrDefault(u => u.IncidentAlertId == reliefEffort.IncidentAlertId);

            reliefEffort.Volunteer = volunteer;
            reliefEffort.IncidentAlert = incident;
            try
                {
                    _context.Update(reliefEffort);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReliefEffortExists(reliefEffort.ReliefEffortId))
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

        // GET: ReliefEfforts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reliefEffort = await _context.ReliefEfforts
                .Include(r => r.IncidentAlert)
                .Include(r => r.Volunteer)
                .FirstOrDefaultAsync(m => m.ReliefEffortId == id);
            if (reliefEffort == null)
            {
                return NotFound();
            }

            return View(reliefEffort);
        }

        // POST: ReliefEfforts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reliefEffort = await _context.ReliefEfforts.FindAsync(id);
            if (reliefEffort != null)
            {
                _context.ReliefEfforts.Remove(reliefEffort);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReliefEffortExists(int id)
        {
            return _context.ReliefEfforts.Any(e => e.ReliefEffortId == id);
        }
    }
}
