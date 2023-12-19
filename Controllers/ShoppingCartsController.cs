using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarioAuth;
using MarioAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace MarioAuth.Controllers
{
    [Authorize]
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public ShoppingCartsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager= userManager;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            string currentUser = _userManager.GetUserId(User);
            var applicationDbContext = _context.ShoppingCart.Include(p => p.Product).Where(u => u.UserId==currentUser);
            ViewBag.TotalCost = TotalPrice();
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult ChangeQuantity(int cartItemId, string action)
        {
            var cartItem = _context.ShoppingCart.Find(cartItemId);

            if (cartItem != null)
            {
                if (action=="add")
                {
                    cartItem.Quantity++;
                }

                if (action=="remove" && cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;

                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public decimal TotalPrice()
        {
            string currentUser = _userManager.GetUserId(User);
            List<ShoppingCart> itrmsInCart = _context.ShoppingCart
           .Include(item => item.Product)
           .Where(item => item.UserId == currentUser)
           .ToList();
            decimal totalCost = itrmsInCart.Sum(item => item.Quantity * Convert.ToDecimal(item.Product.Price));

            return totalCost;
        }

        /*  [HttpPost]
          public IActionResult IncreaseQuantity(int cartItemId)
          {
              var cartItem = _context.ShoppingCart.Find(cartItemId);

              if (cartItem != null)
              {
                  cartItem.Quantity++;
                  _context.SaveChanges();
              }

              return RedirectToAction("Index");
          }

          [HttpPost]
          public IActionResult DecreaseQuantity(int cartItemId)
          {
              var cartItem = _context.ShoppingCart.Find(cartItemId);

              if (cartItem != null && cartItem.Quantity > 1)
              {
                  cartItem.Quantity--;
                  _context.SaveChanges();
              }

              return RedirectToAction("Index");
          }*/


        [HttpPost]
        public ActionResult AddToCart(int productId)
        {
            string currentUser = _userManager.GetUserId(User);
            Create(productId, currentUser);
            //return Redirect("/Home/Index");
            return Json(new { result = "Success" });
        }

        public bool Create(int productId, string userId)
        {
            var newCartItem = new ShoppingCart
            {
                UserId = userId,
                Quantity = 1,
                ProductId=productId,
                DateCreated= DateTime.Now
            };

            _context.ShoppingCart.Add(newCartItem);

            // Сохраняем изменения в базу данных
            _context.SaveChanges();
            return true;
        }


        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingCart = await _context.ShoppingCart
               .Include(s => s.Product)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            if (_context.ShoppingCart == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ShoppingCart'  is null.");
            }
            //var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCart.Remove(shoppingCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
