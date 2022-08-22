using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gymbokning.Data;
using Gymbokning.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Gymbokning.Models.ViewModels;

namespace Gymbokning.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GymClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

		// GET: GymClasses
		public async Task<IActionResult> Index()
		{
			GymClassesIndexViewModel viewModel = new()
			{
				OldGymClasses = await _context.GymClass.Include(c => c.AttendingMembers).Where(c => c.StartTime < DateTime.Now).ToListAsync(),
				UpcomingGymClasses = await _context.GymClass.Include(c => c.AttendingMembers).Where(c => c.StartTime >= DateTime.Now).ToListAsync()
			};

              return _context.GymClass != null ? 
                          View(viewModel) :
                          Problem("Entity set 'ApplicationDbContext.GymClass'  is null.");
        }

		// GET: GymClasses/Details/5
		[Authorize]
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GymClass == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClass
				.Include(c => c.AttendingMembers)
				.ThenInclude(m => m.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

		// GET: GymClasses/Create
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([Bind("Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

		// GET: GymClasses/Edit/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GymClass == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClass.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
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
            return View(gymClass);
        }

		// GET: GymClasses/Delete/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GymClass == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GymClass == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GymClass'  is null.");
            }
            var gymClass = await _context.GymClass.FindAsync(id);
            if (gymClass != null)
            {
                _context.GymClass.Remove(gymClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
          return (_context.GymClass?.Any(e => e.Id == id)).GetValueOrDefault();
        }

		[Authorize]
		public async Task<IActionResult> BookingToggle(int? id)
		{
			if (id == null) return NotFound();

			var gymClass = _context.GymClass!.Include(c => c.AttendingMembers).FirstOrDefault(c => c.Id == id);

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = _context.Users.Find(userId);

			var booking = gymClass.AttendingMembers.FirstOrDefault(m => m.ApplicationUserId == userId);

			var userAlreadyBooked = booking != null;

			if (userAlreadyBooked)
				gymClass.AttendingMembers.Remove(booking);
			else
				gymClass.AttendingMembers.Add(new ApplicationUserGymClass() { GymClass = gymClass, Member = user});

			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));

		}

		public async Task<IActionResult> Booked()
		{
			if (_context.GymClass == null)
				Problem("Entity set 'ApplicationDbContext.GymClass'  is null.");

			var classes = await _context.GymClass.Include(c => c.AttendingMembers).ToListAsync();

			var results = classes.Where(c => c.AttendingMembers.FirstOrDefault(m => m.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)) != null);

			return View(results);
						
		}

		public async Task<IActionResult> History()
		{
			if (_context.GymClass == null)
				Problem("Entity set 'ApplicationDbContext.GymClass'  is null.");

			var classes = await _context.GymClass.Include(c => c.AttendingMembers).ToListAsync();

			var results = classes.Where(c => c.StartTime < DateTime.Now).Where(c => c.AttendingMembers.FirstOrDefault(m => m.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)) != null);

			return View(results);
		}

    }
}
