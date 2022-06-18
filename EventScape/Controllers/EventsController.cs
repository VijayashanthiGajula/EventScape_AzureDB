using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventScape.Data;
using EventScape.Models;
using EventScape.Core;
using Microsoft.AspNetCore.Authorization;
using EventScape.ViewModels;
using System.Security.Claims;
using EventScape.Core.Repository;
using Microsoft.AspNetCore.Identity;
using EventScape.Areas.Identity.Data;

namespace EventScape.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public readonly IUnitOfWork _UnitOfWork;
        private readonly UserManager<ApplicationUser> _UserManager;

        public EventsController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment,IUnitOfWork unitOfWork)
        {
            _context = context;
            _hostEnvironment = hostEnvironment; 
            _UnitOfWork = unitOfWork;
        }

        // GET: Events  ******   Admin  ***********   
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Index(string searchString)
        {

            ViewData["CurrentFilter"] = searchString;
            var Events = from s in _context.Events
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Events = Events.Where(s => s.Location.Contains(searchString));
            }
            return _context.Events != null ?

                         View(await Events.AsNoTracking().ToListAsync()) :
                         Problem("Entity set 'ApplicationDbContext.Events'  is null.");
        }

      
        
        //Get: Upcoming Events******   User  ***********   
        public async Task<IActionResult> UpcomingEvents(string searchString)
        {

            ViewData["CurrentFilter"] = searchString;
            var Events = from s in _context.Events
                         select s;

            if (Events!=null)
            {
                Events = Events.Where(s => s.ShowStartDate>=DateTime.Now);
                if (!String.IsNullOrEmpty(searchString))
                {
                    Events = Events.Where(s => s.Location.Contains(searchString));
                }
            }
            return _context.Events != null ?

                         View(await Events.AsNoTracking().ToListAsync()) :
                         Problem("Entity set 'ApplicationDbContext.Events'  is null.");
        }


        // GET: Events/Details/5  ******   Admin  *********** 
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var events = await _context.Events.FirstOrDefaultAsync(m => m.ID == id);                         
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // GET: Events/Details/5*****************  User details  *************************************************************************************************************
        public async Task<IActionResult> UserEventDetails(int? eventId)
        {

            //if (id == null || _context.Events == null)
            //{
            //    return NotFound();
            //}

            //var events = await _context.Events.FirstOrDefaultAsync(m => m.ID == id);
            //if (events == null)
            //{
            //    return NotFound();
            //}

            //return View(events);
            // ********************* Shoppingcart.cs approach******************8

            WishList Obj = new WishList()
            {
                Event = await _context.Events.FirstOrDefaultAsync(m => m.ID == eventId),
                Tickets = 1,
                EventId = (int)eventId
            };            
            return View(Obj);
        }


        // Post: Events/Details/5*****************   User details   ***************************************************************************************
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> UserEventDetails(WishList WishListobj)
        {
                //adding event to wishlist
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                WishListobj.UserId = claim.Value;
                WishList cartfromDb = _UnitOfWork.WishList.GetFirstOrDefault(u => u.UserId == claim.Value
                && u.EventId == WishListobj.EventId);
            if(cartfromDb == null)
            {
                _UnitOfWork.WishList.Add(WishListobj);
            }
            else
            {
                _UnitOfWork.WishList.IncrementTickets(cartfromDb, WishListobj.Tickets);

            }
               
                _UnitOfWork.Save();                
                 return RedirectToAction(nameof(UpcomingEvents));

        }

        // GET: Events/Create
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Create(EventsViewModel events)
        {
            if (ModelState.IsValid)
            {
                string EventPosterName = null;
            if (events.EventPosters != null)
            {             
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(events.EventPosters.FileName);
                string extension = Path.GetExtension(events.EventPosters.FileName);
                  EventPosterName = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await events.EventPosters.CopyToAsync(fileStream);
                }
                    Events EventsModelObj = new Events
                    {

                        EventName = events.EventName,
                        ShowStartDate = events.ShowStartDate,
                        // ShowEndDate = events.ShowEndDate,
                        Location = events.Location,
                        MaxCapacity = events.MaxCapacity,
                        Description = events.Description,
                        Price = events.Price,
                        EventPosterName = EventPosterName,
                        InitialCapacity = events.MaxCapacity


                    };
                    _context.Add(EventsModelObj);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }          
                
            }
            return View(events);
        }

        // GET: Events/Edit/5 ******************************* Admin Edit   ***********************************************8
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Edit(int? id)
    {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var currentRecord = await _context.Events.FindAsync(id);          
            EventsEditModel EventsModelObj = new EventsEditModel
            {
                Id = currentRecord.ID,
                EventName = currentRecord.EventName,
                ShowStartDate = currentRecord.ShowStartDate,
               // ShowEndDate = currentRecord.ShowEndDate,
                Location = currentRecord.Location,
                MaxCapacity = currentRecord.MaxCapacity,
                Description = currentRecord.Description,
                Price = (decimal)currentRecord.Price,
                ExistingImagePath = currentRecord.EventPosterName,                        
             };
            if (currentRecord == null)
            {
                return NotFound();
            }
            return View(EventsModelObj);
        }

        // POST: Events/Edit/5***************************   Admin Edit  ********************************************************88
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Edit( EventsEditModel events)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Events e = await _context.Events.FindAsync(events.Id);
                    e.EventPosterName = events.ExistingImagePath;
                    e.EventName = events.EventName;
                    e.ShowStartDate = events.ShowStartDate;
//e.ShowEndDate = events.ShowEndDate;
                    e.Location = events.Location;
                    e.MaxCapacity = events.MaxCapacity;
                    e.Description = events.Description;
                    e.Price = events.Price;

                    if (events.EventPosters != null)
                    {                      
                        if (events.ExistingImagePath != null)
                        {
                            string filePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", events.ExistingImagePath);

                            System.IO.File.Delete(filePath);
                        }
                        e.EventPosterName = ProcessUploadedFile(events);
                    }
                  

                    _context.Update(e);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.Id))
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
            return View(events);
        }
        // Method to save edited photo in wwwroot
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        private string ProcessUploadedFile(EventsEditModel model)
        {
            string EventPosterName = null;

        if (model.EventPosters != null)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(model.EventPosters.FileName);
            string extension = Path.GetExtension(model.EventPosters.FileName);
                EventPosterName= filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Image", filename);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                 model.EventPosters.CopyToAsync(fileStream);
            }

        }
        return EventPosterName;
    }
        // Method to save edited photo in wwwroot


        // GET: Events/Delete/5
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .FirstOrDefaultAsync(m => m.ID == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }
            var eventsmodel = await _context.Events.FindAsync(id);
            //delete image from root folder
            var imagepath = Path.Combine(_hostEnvironment.WebRootPath, "Image", eventsmodel.EventPosterName);
            if(System.IO.File.Exists(imagepath))
                System.IO.File.Delete(imagepath);
            //delete record
            if (eventsmodel != null)
            {
                _context.Events.Remove(eventsmodel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsExists(int id)
        {
          return (_context.Events?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
