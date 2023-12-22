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
    public class MasterClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MasterClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MasterClasses
        public async Task<IActionResult> Index()
        {
            return _context.MasterClass != null ?
                        View(await _context.MasterClass.ToListAsync()) :
                        Problem("Нет записей в таблице MasterClass'.");
        }
    }
}
