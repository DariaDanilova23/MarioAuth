using MarioAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace MarioAuth.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager= userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();

        }

        public async Task<IActionResult> Confirmation()
        {
            return View();

        }

        // POST: OrderController/Create
        [HttpPost]
        public ActionResult Create(string tel, string delivery, string address, string comment)
        {
            try
            {
                var addresShop = "";
                if (delivery == "shop")
                {
                    addresShop = "Самовывоз из магазина на Тараса Шевченко";
                }
                else
                {
                    addresShop = address;
                }
                string currentUserEmail = _userManager.GetUserId(User);
                var newOrder = new Order
                {
                    UserId = currentUserEmail,
                    Phone = tel,
                    OrderList =getOrderList(),
                    DeliveryAddress =addresShop,
                    Comment = comment
                };

                _context.Order.Add(newOrder);

                // Сохраняем изменения в базу данных
                _context.SaveChanges();
                return RedirectToAction("Confirmation");
                //return Json(new { result = "Success" });
            }
            catch
            {
                //ОШИБКА
                return View();
            }
        }

        /* public ActionResult Create( string phone, string address, string comment)
         {
             try
             {
                 string currentUserEmail = _userManager.GetUserId(User);
                 var newOrder = new Order
                 {
                     UserId = currentUserEmail,
                     Phone = phone,
                     OrderList =getOrderList(),
                     DeliveryAddress =address,
                     Comment = comment
                 };

                 _context.Order.Add(newOrder);

                 // Сохраняем изменения в базу данных
                 _context.SaveChanges();
                 return RedirectToAction("Confirmation");
                 //return Json(new { result = "Success" });
             }
             catch
             {
                 //ОШИБКА
                 return View();
             }
         }*/



        public string getOrderList()
        {
            string orderList = "";
            string currentUser = _userManager.GetUserId(User);
            //.GetUserId(User);

            var shoppingList = _context.ShoppingCart.Include(p => p.Product).Where(u => u.UserId==currentUser).ToList();
            foreach (var shopping in shoppingList)
            {
                orderList += shopping.Product.Name+"-"+shopping.Quantity+" шт.\n";

            }

            return orderList;
        }
    }
}
