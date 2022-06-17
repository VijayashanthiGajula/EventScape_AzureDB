using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventScape.Data;
using EventScape.Models;
using EventScape.Core.Repository;
using EventScape.Core;
using EventScape.ViewModels;

namespace EventScape.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public BookingsViewModel BookingsVM { get; set; }
        public BookingsController(ApplicationDbContext context, IUnitOfWork unitOfWOrk)
        {
            _context = context;
            _unitOfWork = unitOfWOrk;
        }

        // GET: Users page- booking history
        public async Task<IActionResult> UserBookingHistory()
        {
            var applicationDbContext = _context.Booking.Include(b => b.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admins page- booking history
        public async Task<IActionResult> AdminBookingRequests()
        { 
            
            BookingsViewModel bookingReq = new BookingsViewModel()
            {
                newBookingRequests = _unitOfWork.Booking.GetAll(b=>b.BookingStatus==Constants.Status.BookingPending),
                                    
                ConfirmedBookingRequests = _unitOfWork.Booking.GetAll(b => b.BookingStatus == Constants.Status.BookingConfirmed)
                                   
            };

            return View(bookingReq);
        }
        

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,BookingDate,OrderTotal,BookingStatus,PhoneNumber,StreetAddress,City,State,PostalCode,Name")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", booking.ApplicationUserId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", booking.ApplicationUserId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        public async Task<IActionResult> BookingConfirmation(int id)
        {
            
            Booking booking= await _context.Booking.FindAsync(id);
            booking.BookingStatus = Constants.Status.BookingConfirmed;
            _context.Update(booking);
            _context.SaveChanges();
            return RedirectToAction(nameof(AdminBookingRequests));
            
        }
       

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Booking == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Booking'  is null.");
            }
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
          return (_context.Booking?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
