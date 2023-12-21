using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarioAuth;
using MarioAuth.Models;

namespace MarioAuth.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 5; // Количество новостей на одной странице
            int pageNumber = page ?? 1; // Номер текущей страницы

            var news = _context.News.ToList(); // Получение всех новостей (замените на ваш запрос данных)
            if (news == null) return NotFound();
            
            var paginatedNews = news.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new NewsViewModel
            {
                News = paginatedNews,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalItems = news.Count
            };

            return View(viewModel);
        }
        /*
        public async Task<IActionResult> Index()
        {
              return _context.News != null ? 
                          View(await _context.News.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.News'  is null.");
        }
      */
    }
}
