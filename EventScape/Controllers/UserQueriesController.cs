using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventScape.Data;
using EventScape.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EventScape.Core.Repository;
using EventScape.Core;
using EventScape.ViewModels;

namespace EventScape.Controllers
{
    public class UserQueriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly IUnitOfWork _UnitOfWork;
        public UserQueriesController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _UnitOfWork = unitOfWork;
        }
        public UserQueriesViewModel UserQueriesViewModel { get; set; }
        // GET: UserQueries
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.UserQueries
                .Include(u => u.ApplicationUser)
                .Include(u => u.Events)
                .Where(u=>u.UserId==claim.Value);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> AdminUserQueries()
        {
            var applicationDbContext = _context.UserQueries.Include(u => u.ApplicationUser).Include(u => u.Events);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserQueries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserQueries == null)
            {
                return NotFound();
            }

            var userQueries = await _context.UserQueries
                .Include(u => u.ApplicationUser)
                .Include(u => u.Events)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userQueries == null)
            {
                return NotFound();
            }

            return View(userQueries);
        }
        // GET:UserQueryById
        public async Task<IActionResult> UserQueryByEventId(int? eventId)
        {
            

            UserQueries Obj = new UserQueries()
            {
                Events = await _context.Events.FirstOrDefaultAsync(m => m.ID == eventId),
              
                EventId = (int)eventId
            };
            return View(Obj);
        }

        // Post: Events/Details/5*****************   User details   ***************************************************************************************
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> UserQueryById(UserQueries Obj)
        {
            //Adding a query by event 
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var events = await _context.Events.FirstOrDefaultAsync(m => m.ID == Obj.EventId);

            Obj.UserId = claim.Value;
            Obj.DatePosted = DateTime.Now;
           Obj.Reply = Constants.Status.QueryStatusActive;
            Obj.Status = Constants.Status.QueryStatusActive;
          Obj.Events= events;

            UserQueries cartfromDb = _UnitOfWork.UserQueries.GetFirstOrDefault(u => u.UserId == claim.Value
            && u.EventId == Obj.EventId);
            if (cartfromDb == null)
            {
                _UnitOfWork.UserQueries.Add(Obj);
            }
            else if(cartfromDb.DatePosted==Obj.DatePosted)
            {
                _UnitOfWork.UserQueries.append(cartfromDb, cartfromDb.Query, Obj.Query);

            }
            else
            {
                _UnitOfWork.UserQueries.Add(Obj); 
            }

            _UnitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }








        // GET: UserQueries/Create
        [Authorize(Roles = $"{Constants.Roles.UserRole}")]
        public IActionResult Create()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);            
            ViewData["UserId"] = claim.Value;
            ViewData["EventId"] = new SelectList(_context.Events, "ID", "EventName");
            return View();
        }

        [HttpPost]
      
        [Authorize]
        [Authorize(Roles = $"{Constants.Roles.UserRole}")]
        public async Task<IActionResult> Create(UserQueries Obj)
        {
            //Adding a query by event 
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var events = await _context.Events.FirstOrDefaultAsync(m => m.ID == Obj.EventId);

            Obj.UserId = claim.Value;
            Obj.DatePosted = DateTime.Now;
            Obj.Reply = Constants.Status.QueryStatusActive;
            Obj.Status = Constants.Status.QueryStatusActive;
            Obj.Events = events;

            UserQueries cartfromDb = _UnitOfWork.UserQueries.GetFirstOrDefault(u => u.UserId == claim.Value
            && u.EventId == Obj.EventId);
            if (cartfromDb == null)
            {
                _UnitOfWork.UserQueries.Add(Obj);
            }
            else if (cartfromDb.DatePosted == Obj.DatePosted)
            {
                _UnitOfWork.UserQueries.append(cartfromDb, cartfromDb.Query, Obj.Query);

            }
            else
            {
                _UnitOfWork.UserQueries.Add(Obj);
            }

            _UnitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }

        // GET: UserQueries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserQueries == null)
            {
                return NotFound();
            }

            var userQueries = await _context.UserQueries.FindAsync(id);
            if (userQueries == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userQueries.UserId);
            ViewData["EventId"] = new SelectList(_context.Events, "ID", "EventName", userQueries.EventId);
            return View(userQueries);
        }

        // POST: UserQueries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EventId,UserId,Query,DatePosted,Reply,Status")] UserQueries userQueries)
        {
            if (id != userQueries.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userQueries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserQueriesExists(userQueries.ID))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userQueries.UserId);
            ViewData["EventId"] = new SelectList(_context.Events, "ID", "EventName", userQueries.EventId);
            return View(userQueries);
        }

        // GET: UserQueries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserQueries == null)
            {
                return NotFound();
            }

            var userQueries = await _context.UserQueries
                .Include(u => u.ApplicationUser)
                .Include(u => u.Events)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userQueries == null)
            {
                return NotFound();
            }

            return View(userQueries);
        }

        // POST: UserQueries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserQueries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserQueries'  is null.");
            }
            var userQueries = await _context.UserQueries.FindAsync(id);
            if (userQueries != null)
            {
                _context.UserQueries.Remove(userQueries);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserQueriesExists(int id)
        {
          return (_context.UserQueries?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
