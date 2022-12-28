using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBook.Models;
using FPTBook.Data;
using System.Security.Principal;

namespace FPTBook.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(String searchString)
        {
            var accounts = from a in _context.Accounts select a;
            if(!String.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(x => x.Name.Contains(searchString));
            }
            return View(await accounts.ToListAsync());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Name,Role,Username,Password,ProfilePicture")] Account account)
        {
            if(ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null || _context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if(account == null)
            {
                return NotFound();
            }
            return View(account);
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name,Role,Username,Password,ProfilePicture")] Account account)
        {
            if(id != account.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));  
            }
            return View(account);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            if(account == null)
            {
                return NotFound();
            }
            return View(account);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeleteConfirmed(int id)
        {
            if(_context.Accounts == null)
            {
                return Problem("Entity set 'FPTBookStore.Account' is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if(account != null)
            {
                _context.Accounts.Remove(account);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CheckReq()
        {
            var categoryReqs = from c in _context.CategoryReqs select c;
            return View(await categoryReqs.ToListAsync());
        }

        public async Task<IActionResult> Accept(int? id)
        {
            if (id == null || _context.CategoryReqs == null)
            {
                return NotFound();
            }
            var categoryReq = await _context.CategoryReqs.FindAsync(id);
            if (categoryReq == null)
            {
                return NotFound();
            }
            return View(categoryReq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id, [Bind("Id, Name, Category, CategoryReq")] CategoryReq categoryReq, Book book)
        {
            if (id != categoryReq.Id)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            _context.Add(book);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AccountExists(categoryReq.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return View(book);
        }
        public async Task<IActionResult> Decline(int? id)
        {
            if (id == null || _context.CategoryReqs == null)
            {
                return NotFound();
            }
            var categoryReq = await _context.CategoryReqs.FirstOrDefaultAsync(a => a.Id == id);
            if (categoryReq == null)
            {
                return NotFound();
            }
            return View(categoryReq);
        }
        [HttpPost, ActionName("Decline")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeclineConfirmed(int id)
        {
            if (_context.CategoryReqs == null)
            {
                return Problem("Entity set 'FPTBookStore.Account' is null.");
            }
            var categoryReq = await _context.CategoryReqs.FindAsync(id);
            if (categoryReq != null)
            {
                _context.CategoryReqs.Remove(categoryReq);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
