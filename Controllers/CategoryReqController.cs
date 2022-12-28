using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FPTBook.Models;
using FPTBook.Data;

namespace FPTBook.Controllers
{
    public class CategoryReqController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryReqController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoryReqs.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Req")] CategoryReq categoryreq)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryreq);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryreq);
        }
    }
}
