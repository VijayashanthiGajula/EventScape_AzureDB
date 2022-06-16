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

namespace EventScape.Controllers
{
    public class WishListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
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
                
            };
            foreach( var item in ShoppingCartViewModel.CartItems)
            {
                ShoppingCartViewModel.CartTotal += (item.Event.Price * item.Tickets);
            }
           
            return View(ShoppingCartViewModel);  

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
            _unitOfWork.WishList.DecrementTickets(wishList, 1);
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
