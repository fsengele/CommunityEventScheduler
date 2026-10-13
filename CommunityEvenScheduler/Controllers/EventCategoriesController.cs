
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommunityEventScheduler.Models;
using CommunityEventScheduler.Data;

public class EventCategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventCategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: EVENTCATEGORYS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.EventCategories.ToListAsync());
    }

    // GET: EVENTCATEGORYS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eventcategory = await _context.EventCategories
            .FirstOrDefaultAsync(m => m.Id == id);
        if (eventcategory == null)
        {
            return NotFound();
        }

        return View(eventcategory);
    }

    // GET: EVENTCATEGORYS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: EVENTCATEGORYS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] EventCategory eventcategory)
    {
        if (ModelState.IsValid)
        {
            _context.Add(eventcategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(eventcategory);
    }

    // GET: EVENTCATEGORYS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eventcategory = await _context.EventCategories.FindAsync(id);
        if (eventcategory == null)
        {
            return NotFound();
        }
        return View(eventcategory);
    }

    // POST: EVENTCATEGORYS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name")] EventCategory eventcategory)
    {
        if (id != eventcategory.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(eventcategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventCategoryExists(eventcategory.Id))
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
        return View(eventcategory);
    }

    // GET: EVENTCATEGORYS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eventcategory = await _context.EventCategories
            .FirstOrDefaultAsync(m => m.Id == id);
        if (eventcategory == null)
        {
            return NotFound();
        }

        return View(eventcategory);
    }

    // POST: EVENTCATEGORYS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var eventcategory = await _context.EventCategories.FindAsync(id);
        if (eventcategory != null)
        {
            _context.EventCategories.Remove(eventcategory);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EventCategoryExists(int? id)
    {
        return _context.EventCategories.Any(e => e.Id == id);
    }
}
