using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Places.DAL.EF;
using Places.Domain;

namespace Places.Web.Controllers
{
    public class FacilitiesController : Controller
    {
        private readonly PlacesContext _context;

        public FacilitiesController(PlacesContext context)
        {
            _context = context;
        }

        // GET: Facilities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Facilities.ToListAsync());
        }

        // GET: Facilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilitie = await _context.Facilities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (facilitie == null)
            {
                return NotFound();
            }

            return View(facilitie);
        }

        // GET: Facilities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] Facilitie facilitie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facilitie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facilitie);
        }

        // GET: Facilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilitie = await _context.Facilities.SingleOrDefaultAsync(m => m.Id == id);
            if (facilitie == null)
            {
                return NotFound();
            }
            return View(facilitie);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] Facilitie facilitie)
        {
            if (id != facilitie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facilitie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilitieExists(facilitie.Id))
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
            return View(facilitie);
        }

        // GET: Facilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilitie = await _context.Facilities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (facilitie == null)
            {
                return NotFound();
            }

            return View(facilitie);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facilitie = await _context.Facilities.SingleOrDefaultAsync(m => m.Id == id);
            _context.Facilities.Remove(facilitie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilitieExists(int id)
        {
            return _context.Facilities.Any(e => e.Id == id);
        }
    }
}
