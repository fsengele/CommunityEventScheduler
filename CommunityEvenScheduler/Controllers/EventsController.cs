
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunityEventScheduler.Models;
using CommunityEventScheduler.Data;

namespace CommunityEventScheduler.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Events
        // Display and filter community events.
        public async Task<IActionResult> Index(
            string? city,
            int? categoryId,
            DateTime? eventDate)
        {
            // Start with all events.
            var events = _context.Events
                .Include(e => e.Location)
                .Include(e => e.EventCategory)
                .AsQueryable();

            // Filter by city.
            if (!string.IsNullOrWhiteSpace(city))
            {
                events = events.Where(e =>
                    e.Location != null &&
                    e.Location.City == city);
            }

            // Filter by event category.
            if (categoryId.HasValue)
            {
                events = events.Where(e =>
                    e.EventCategoryId == categoryId.Value);
            }

            // Filter by event date.
            if (eventDate.HasValue)
            {
                DateTime selectedDate = eventDate.Value.Date;

                events = events.Where(e =>
                    e.EventDate.Date == selectedDate);
            }

            // Get cities for the search dropdown.
            ViewBag.Cities = await _context.Locations
                .Select(l => l.City)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            // Get categories for the search dropdown.
            ViewBag.Categories = await _context.EventCategories
                .OrderBy(c => c.Name)
                .ToListAsync();

            // Keep selected search values.
            ViewBag.SelectedCity = city;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SelectedEventDate = eventDate;

            // Get matching events.
            var result = await events
                .OrderBy(e => e.EventDate)
                .ToListAsync();

            return View(result);
        }


        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventItem = await _context.Events
                .Include(e => e.Location)
                .Include(e => e.EventCategory)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
            {
                return NotFound();
            }

            return View(eventItem);
        }


        // GET: Events/Create
        public IActionResult Create()
        {
            PopulateDropdowns();

            return View();
        }


        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,EventDate,StartTime,EndTime,Description,Organizer,LocationId,EventCategoryId")]
            Event eventItem)
        {
            // Location and EventCategory are selected by IDs.
            ModelState.Remove(nameof(Event.Location));
            ModelState.Remove(nameof(Event.EventCategory));

            if (ModelState.IsValid)
            {
                _context.Add(eventItem);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(eventItem);

            return View(eventItem);
        }


        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            PopulateDropdowns(eventItem);

            return View(eventItem);
        }


        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int? id,
            [Bind("Id,Title,EventDate,StartTime,EndTime,Description,Organizer,LocationId,EventCategoryId")]
            Event eventItem)
        {
            if (id != eventItem.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(Event.Location));
            ModelState.Remove(nameof(Event.EventCategory));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventItem);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventItem.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(eventItem);

            return View(eventItem);
        }


        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventItem = await _context.Events
                .Include(e => e.Location)
                .Include(e => e.EventCategory)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
            {
                return NotFound();
            }

            return View(eventItem);
        }


        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem != null)
            {
                _context.Events.Remove(eventItem);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        // Fill Location and Category dropdowns.
        private void PopulateDropdowns(Event? eventItem = null)
        {
            var locations = _context.Locations
                .Select(location => new
                {
                    location.Id,
                    Name = location.City + ", " + location.State
                })
                .ToList();

            ViewData["LocationId"] = new SelectList(
                locations,
                "Id",
                "Name",
                eventItem?.LocationId
            );

            ViewData["EventCategoryId"] = new SelectList(
                _context.EventCategories,
                "Id",
                "Name",
                eventItem?.EventCategoryId
            );
        }


        private bool EventExists(int? id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
