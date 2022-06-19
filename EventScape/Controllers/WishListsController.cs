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
using EventScape.Core.Repository;
using EventScape.ViewModels;
using EventScape.Core;
using Microsoft.AspNetCore.Authorization;

namespace EventScape.Controllers
{
    [Authorize(Roles = $"{Constants.Roles.UserRole}")]
    public class WishListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
       
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
        public Events Events { get; set; }
        public WishListsController(ApplicationDbContext context, IUnitOfWork unitOfWOrk)
        {
            _context = context;
            _unitOfWork = unitOfWOrk;   
        }

        // GET: WishLists
        public async Task<IActionResult> Index()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                CartItems = _unitOfWork.WishList.GetAll(u => u.UserId == claim.Value, includeProperties: "Event"),
                Booking = new()
            };
            foreach( var item in ShoppingCartViewModel.CartItems)
            {
                ShoppingCartViewModel.Booking.OrderTotal += (item.Event.Price * item.Tickets);
            }
           
            return View(ShoppingCartViewModel);  

        }
        //Get: Summary
        public async Task<IActionResult> Summary()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);



            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                CartItems = _unitOfWork.WishList.GetAll(u => u.UserId == claim.Value, includeProperties: "Event"),
                Booking=new()
            };
            ShoppingCartViewModel.Booking.ApplicationUser = _unitOfWork.User.GetUserById(claim.Value);
            ShoppingCartViewModel.Booking.Name = ShoppingCartViewModel.Booking.ApplicationUser.FirstName + ShoppingCartViewModel.Booking.ApplicationUser.LastName;
            ShoppingCartViewModel.Booking.PhoneNumber = ShoppingCartViewModel.Booking.ApplicationUser.PhoneNumber;
            ShoppingCartViewModel.Booking.Email = ShoppingCartViewModel.Booking.ApplicationUser.Email; 

            foreach (var item in ShoppingCartViewModel.CartItems)
            {
                ShoppingCartViewModel.Booking.OrderTotal += (item.Event.Price * item.Tickets);
            }

            return View(ShoppingCartViewModel);
            //return View();

        }

        //Post Summary
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SummaryPost(ShoppingCartViewModel ShoppingCartViewModel)
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartViewModel.CartItems = _unitOfWork.WishList.GetAll(u => u.UserId == claim.Value, includeProperties: "Event");
            ShoppingCartViewModel.Booking.BookingStatus = Constants.Status.BookingPending;
            ShoppingCartViewModel.Booking.BookingDate = System.DateTime.Now;
            ShoppingCartViewModel.Booking.ApplicationUserId = claim.Value;


            foreach (var item in ShoppingCartViewModel.CartItems)
            {
                ShoppingCartViewModel.Booking.OrderTotal += (item.Event.Price * item.Tickets);
            }
            _unitOfWork.Booking.Add(ShoppingCartViewModel.Booking);
            _unitOfWork.Save();


            foreach (var item in ShoppingCartViewModel.CartItems)
            {
                BookingDetails bookingDetails = new BookingDetails()
                {
                    EventId = item.EventId,
                    BookingID = ShoppingCartViewModel.Booking.Id,
                    UnitPrice = item.Price,
                    No_Of_Tickets = item.Tickets

                };

                _unitOfWork.BookingDetails.Add(bookingDetails);
                _unitOfWork.Save();
                Events E = _context.Events.FirstOrDefault(i => i.ID == item.EventId);
                E.MaxCapacity = E.MaxCapacity - item.Tickets;
                _context.Events.Update(E);
                _context.SaveChanges();

            }
            //Removing executed booking
            _unitOfWork.WishList.RemoveRange(ShoppingCartViewModel.CartItems);
            _unitOfWork.Save();
         
            return RedirectToAction("Index","Home");

        }
        // Increment: WishLists/Plus
        public async Task<IActionResult> Plus(int? id)
        {
            if (id == null || _context.WishList == null)
            {
                return NotFound();
            }

            var wishList =_unitOfWork.WishList.GetFirstOrDefault(u=>u.WishListId==id);
            if (wishList == null)
            {
                return NotFound();
            }
            _unitOfWork.WishList.IncrementTickets(wishList, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
           
        }
        // Decrement: WishLists/Minus
        public async Task<IActionResult> Minus(int? id)
        {
            if (id == null || _context.WishList == null)
            {
                return NotFound();
            }

            var wishList = _unitOfWork.WishList.GetFirstOrDefault(u => u.WishListId == id);
            if (wishList == null)
            {
                return NotFound();
            }
            if(wishList.Tickets<=1)
            {
                _unitOfWork.WishList.Remove(wishList);
            }
            else { 
            _unitOfWork.WishList.DecrementTickets(wishList, 1);
           
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
             
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WishList == null)
            {
                return NotFound();
            }

            var wishList = _unitOfWork.WishList.GetFirstOrDefault(u => u.WishListId == id);
            if (wishList == null)
            {
                return NotFound();
            }
            _unitOfWork.WishList.Remove(wishList);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }

       

        private bool WishListExists(int id)
        {
          return (_context.WishList?.Any(e => e.WishListId == id)).GetValueOrDefault();
        }
    }
}
