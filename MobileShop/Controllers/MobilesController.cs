using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileShop.Data;
using MobileShop.Models;

namespace MobileShop.Controllers
{
    public class MobilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mobiles
        public async Task<IActionResult> Index()
        {

            ViewData["SellerMobile"] = await _context.SellerMobile.ToListAsync();
            ViewData["Sellers"] = await _context.Seller.ToListAsync();
            var applicationDbContext = _context.Mobile.Include(m => m.Manufacturers);
            return View(await applicationDbContext.ToListAsync());
        }
        /*==================================*/
        public async Task<IActionResult> MobileDetails(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            ViewData["SellerMobile"] = await _context.SellerMobile.ToListAsync();
            ViewData["Sellers"] = await _context.Seller.ToListAsync();
            var applicationDbContext = _context.Mobile.Include(m => m.Manufacturers).Where(m => m.Id == Id);
            return View("Index", await applicationDbContext.ToListAsync());
        }


        /*==================================*/

        
        public IActionResult SearchForm()
        {
            return View();
        }
        public async Task<IActionResult> SearchResult(string Name)
        {
            return View("Index", await _context.Mobile.Where(a => a.Name.Contains(Name)).ToListAsync());
        }
        // GET: Mobiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobile = await _context.Mobile
                .Include(m => m.Manufacturers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobile == null)
            {
                return NotFound();
            }

            return View(mobile);
        }

        // GET: Mobiles/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            ViewData["SellerId"] = new SelectList(_context.Seller, "Id", "Name");
            return View();
        }

        // POST: Mobiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create([Bind("Id,Name,Url,Price,ManufacturerId")] Mobile mobile, List<int> Sellers)
        {
            if (ModelState.IsValid)
            {
                _context.Mobile.Add(mobile);
                await _context.SaveChangesAsync();
                List<SellerMobile> sellerMobile = new List<SellerMobile>();
                foreach (int seller in Sellers)
                {
                    sellerMobile.Add(new SellerMobile { SellerId = seller, MobileId = mobile.Id});
                }
                _context.SellerMobile.AddRange(sellerMobile);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", mobile.ManufacturerId);
            return View(mobile);
        }

        // GET: Mobiles/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobile = await _context.Mobile.FindAsync(id);
            if (mobile == null)
            {
                return NotFound();
            }

            IList<SellerMobile> sellerMobiles = await _context.SellerMobile.Where<SellerMobile>(a => a.MobileId == mobile.Id).ToListAsync();
            IList<int> listsellers = new List<int>();
            foreach (SellerMobile sellerMobile in sellerMobiles)
            {
                listsellers.Add(sellerMobile.SellerId);
            }

            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", mobile.ManufacturerId);
            ViewData["SellerId"] = new MultiSelectList(_context.Seller, "Id", "Name", listsellers.ToArray());
            return View(mobile);
        }

        // POST: Mobiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Url,Price,ManufacturerId")] Mobile mobile, List<int> Sellers)
        {
            var transaction = _context.Database.BeginTransaction();
            if (id != mobile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobile);
                    _context.SaveChanges();
                    List<SellerMobile> sellerMobile = new List<SellerMobile>();
                    foreach (int seller in Sellers)
                    {
                        sellerMobile.Add(new SellerMobile { SellerId = seller, MobileId = mobile.Id });
                    }
                    List<SellerMobile> deleteBookAuthors = await _context.SellerMobile.Where<SellerMobile>(a => a.MobileId == mobile.Id).ToListAsync();
                    _context.SellerMobile.RemoveRange(deleteBookAuthors);
                    _context.SaveChanges();

                    _context.SellerMobile.UpdateRange(sellerMobile);
                    _context.SaveChanges();

                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobileExists(mobile.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", mobile.ManufacturerId);
            return View(mobile);
        }

        // GET: Mobiles/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobile = await _context.Mobile
                .Include(m => m.Manufacturers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobile == null)
            {
                return NotFound();
            }

            return View(mobile);
        }

        // POST: Mobiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mobile = await _context.Mobile.FindAsync(id);
            _context.Mobile.Remove(mobile);
            /* await _context.SaveChangesAsync();*/

            _context.SaveChanges();
            List<SellerMobile> deleteSellerMobiles = await _context.SellerMobile.Where<SellerMobile>(a => a.MobileId == mobile.Id).ToListAsync();
            _context.SellerMobile.RemoveRange(deleteSellerMobiles);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool MobileExists(int id)
        {
            return _context.Mobile.Any(e => e.Id == id);
        }
    }
}
