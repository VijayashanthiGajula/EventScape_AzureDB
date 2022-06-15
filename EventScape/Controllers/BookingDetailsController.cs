using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventScape.Data;
using EventScape.Models;

namespace EventScape.Controllers
{
    public class BookingDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookingDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookingDetails.Include(b => b.Booking).Include(b => b.Event);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookingDetails == null)
            {
                return NotFound();
            }

            var bookingDetails = await _context.BookingDetails
                .Include(b => b.Booking)
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingDetails == null)
            {
                return NotFound();
            }

            return View(bookingDetails);
        }

        // GET: BookingDetails/Create
        public IActionResult Create()
        {
            ViewData["BookingID"] = new SelectList(_context.Set<Booking>(), "Id", "Id");
            ViewData["EventId"] = new SelectList(_context.Events, "ID", "EventName");
            return View();
        }

        // POST: BookingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookingID,EventId,Tickets,Price")] BookingDetails bookingDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingID"] = new SelectList(_context.Set<Booking>(), "Id", "Id", bookingDetails.BookingID);
            ViewData["EventId"] = new SelectList(_context.Events, "ID", "EventName", bookingDetails.EventId);
            return View(bookingDetails);
        }

        // GET: BookingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookingDetails == null)
            {
                return NotFound();
            }

            var bookingDetails = await _context.BookingDetails.FindAsync(id);
            if (bookingDetails == null)
            {
                return NotFound();
            }
            ViewData["BookingID"] = new SelectList(_context.Set<Booking>(), "Id", "Id", bookingDetails.BookingID);
            ViewData["EventId"] = new SelectList(_context.Events, "ID", "EventName", bookingDetails.EventId);
            return View(bookingDetails);
        }

        // POST: BookingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookingID,EventId,Tickets,Price")] BookingDetails bookingDetails)
        {
            if (id != bookingDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingDetailsExists(bookingDetails.Id))
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
            ViewData["BookingID"] = new SelectList(_context.Set<Booking>(), "Id", "Id", bookingDetails.BookingID);
            ViewData["EventId"] = new SelectList(_context.Events, "ID", "EventName", bookingDetails.EventId);
            return View(bookingDetails);
        }

        // GET: BookingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookingDetails == null)
            {
                return NotFound();
            }

            var bookingDetails = await _context.BookingDetails
                .Include(b => b.Booking)
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingDetails == null)
            {
                return NotFound();
            }

            return View(bookingDetails);
        }

        // POST: BookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookingDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BookingDetails'  is null.");
            }
            var bookingDetails = await _context.BookingDetails.FindAsync(id);
            if (bookingDetails != null)
            {
                _context.BookingDetails.Remove(bookingDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingDetailsExists(int id)
        {
          return (_context.BookingDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
