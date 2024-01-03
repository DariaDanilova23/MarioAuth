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
            var applicationDbContext = _context.ShoppingCart.Include(p => p.Product).Where(u => u.UserId==currentUser); //Получение товаров в корзине
            ViewBag.TotalCost = TotalPrice(); //Получение итоговой цены корзины
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult ChangeQuantity(int cartItemId, int quantityValue)
        {
            var cartItem = _context.ShoppingCart.Find(cartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity=quantityValue;
                _context.SaveChanges();
            }
            return Json(new { totalCost = TotalPrice() });
        }

        public decimal TotalPrice()
        {
            string currentUser = _userManager.GetUserId(User);
            List<ShoppingCart> itrmsInCart = _context.ShoppingCart //Получение товаров в корзине у текущего пользователя
           .Include(item => item.Product)
           .Where(item => item.UserId == currentUser)
           .ToList();
            decimal totalCost = itrmsInCart.Sum(item => item.Quantity * Convert.ToDecimal(item.Product.Price)); //Подсчет итоговой цены
            return totalCost;
        }

        [HttpPost]
        public ActionResult AddToCart(int productId)
        {
            string currentUser = _userManager.GetUserId(User); //Получение текущего пользователя
            Create(productId, currentUser);
            return Json(new { result = "Success" });
        }

        public bool Create(int productId, string userId)
        {
            var newCartItem = new ShoppingCart //Добавление товара в корзину
            {
                UserId = userId,
                Quantity = 1,
                ProductId=productId,
                DateCreated= DateTime.Now
            };
            _context.ShoppingCart.Add(newCartItem); //Сохранение данных
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
                return Problem("Нет записей в таблице ShoppingCart.");
            }
            if (shoppingCart != null)
            {
                _context.ShoppingCart.Remove(shoppingCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
