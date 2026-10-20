
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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

        // Everyone can explore community events.
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string? city,
            int? categoryId,
            DateTime? eventDate)
        {
            var events = _context.Events
                .Include(e => e.Location)
                .Include(e => e.EventCategory)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(city))
            {
                events = events.Where(e =>
                    e.Location != null &&
                    e.Location.City == city);
            }

            if (categoryId.HasValue)
            {
                events = events.Where(e =>
                    e.EventCategoryId == categoryId.Value);
            }

            if (eventDate.HasValue)
            {
                DateTime selectedDate = eventDate.Value.Date;

                events = events.Where(e =>
                    e.EventDate.Date == selectedDate);
            }

            ViewBag.Cities = await _context.Locations
                .Select(l => l.City)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            ViewBag.Categories = await _context.EventCategories
                .OrderBy(c => c.Name)
                .ToListAsync();

            ViewBag.SelectedCity = city;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SelectedEventDate = eventDate;

            var result = await events
                .OrderBy(e => e.EventDate)
                .ToListAsync();

            return View(result);
        }

        // Everyone can see event details.
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var eventItem = await _context.Events
                .Include(e => e.Location)
                .Include(e => e.EventCategory)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
                return NotFound();

            return View(eventItem);
        }

        // Only logged-in users can create events.
        [Authorize]
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,EventDate,StartTime,EndTime,Description,Organizer,LocationId,EventCategoryId")]
            Event eventItem)
        {
            ModelState.Remove(nameof(Event.Location));
            ModelState.Remove(nameof(Event.EventCategory));

            if (ModelState.IsValid)
            {
                string? userId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Challenge();

                // The server assigns the event owner.
                eventItem.CreatedByUserId = userId;

                _context.Events.Add(eventItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(eventItem);
            return View(eventItem);
        }

        // Only the owner or an authorized admin can edit.
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
                return NotFound();

            if (!CanManageEvent(eventItem))
                return Forbid();

            PopulateDropdowns(eventItem);
            return View(eventItem);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int? id,
            [Bind("Id,Title,EventDate,StartTime,EndTime,Description,Organizer,LocationId,EventCategoryId")]
            Event eventItem)
        {
            if (id == null || id != eventItem.Id)
                return NotFound();

            // Load the real event from the database.
            var existingEvent = await _context.Events.FindAsync(id);

            if (existingEvent == null)
                return NotFound();

            // Check ownership before making changes.
            if (!CanManageEvent(existingEvent))
                return Forbid();

            ModelState.Remove(nameof(Event.Location));
            ModelState.Remove(nameof(Event.EventCategory));

            if (ModelState.IsValid)
            {
                // Update only allowed fields.
                existingEvent.Title = eventItem.Title;
                existingEvent.EventDate = eventItem.EventDate;
                existingEvent.StartTime = eventItem.StartTime;
                existingEvent.EndTime = eventItem.EndTime;
                existingEvent.Description = eventItem.Description;
                existingEvent.Organizer = eventItem.Organizer;
                existingEvent.LocationId = eventItem.LocationId;
                existingEvent.EventCategoryId = eventItem.EventCategoryId;

                // CreatedByUserId is never changed here.

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(eventItem);
            return View(eventItem);
        }

        // Only the owner or an authorized admin can delete.
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var eventItem = await _context.Events
                .Include(e => e.Location)
                .Include(e => e.EventCategory)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
                return NotFound();

            if (!CanManageEvent(eventItem))
                return Forbid();

            return View(eventItem);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();

            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
                return NotFound();

            if (!CanManageEvent(eventItem))
                return Forbid();

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Check if the logged-in user owns this event.
        // Admin access works when the Admin role is configured.
        private bool CanManageEvent(Event eventItem)
        {
            string? userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return false;

            if (User.IsInRole("Admin"))
                return true;

            return eventItem.CreatedByUserId == userId;
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
    }
}
