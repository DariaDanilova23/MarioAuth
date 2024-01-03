using MarioAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;

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

        public bool ClearCart()
        {
            string currentUser = _userManager.GetUserId(User);

            if (currentUser != null)
            {

                var recordsToDelete = _context.ShoppingCart.Where(p => p.UserId == currentUser).ToList();

                if (recordsToDelete.Any())
                {
                    _context.ShoppingCart.RemoveRange(recordsToDelete);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        public ActionResult Create(string tel, string delivery, string address, string comment = null)
        {
            try
            {
                string addresShop; 
                if (delivery == "shop") //Если из списка выбран самовывоз добавить соответствующую запись в бд
                {
                    addresShop = "Самовывоз из магазина на Тараса Шевченко";
                }
                else //Если доставка добавит введенный адрес
                {
                    addresShop = address;
                }

                string currentUserEmail = _userManager.GetUserId(User);
                var newOrder = new Order //Создание записи о заказе в бд
                {
                    UserId = currentUserEmail,
                    Phone = tel,
                    OrderList = getOrderList(), //Вызов метод для получения списка товаров в корзине
                    DeliveryAddress =addresShop,
                    Comment = comment
                };

                _context.Order.Add(newOrder); //Добавление записи в бд


                _context.SaveChanges();
                ClearCart(); //Метод очистки корзины

                return RedirectToAction("Confirmation");
            }
            catch (Exception ex) //Обработчик исключения 
            {
                TempData["ErrorMessage"] = "Произошла ошибка при создании заказа!";
                return RedirectToAction("Index");
            }
        }

        public string getOrderList()
        {
            string currentUser = _userManager.GetUserId(User);
            StringBuilder sb = new StringBuilder();
            var shoppingList = _context.ShoppingCart.Include(p => p.Product).Where(u => u.UserId==currentUser).ToList(); //Получение списка товаров в корзине 
            foreach (var shopping in shoppingList)
            {
                sb.Append($"{shopping.Product.Name} - {shopping.Quantity} шт.\n");//Создание строки с товарами и их количеством
            }

            return sb.ToString();
        }
    }
}
