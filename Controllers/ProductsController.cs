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

namespace MarioAuth.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        // public string catalog { get; private set; }
        public ProductsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index(int catalogId, string catalogName)
        {
            ViewData["catalogName"]=catalogName; //Вывод названия категирии товаров
            string currentUser = _userManager.GetUserId(User); //Получение Id пользователя в системе
            var products = _context.Product.Where(p => p.CatalogSection == catalogId).ToList(); //Получение списка всех продуктов в выбранной категории 
            var shoppingCart = _context.ShoppingCart.Where(p=>p.UserId==currentUser).ToList(); //Получение корзины пользователя в системе
            var viewModel = new ProductInCart //Определение перемееной, которая хранит товары, которые пользователь добавил в корзину 
            {
                Products = products,
                ShoppingCarts = shoppingCart
            };
            return View(viewModel);
        }

    }
}
