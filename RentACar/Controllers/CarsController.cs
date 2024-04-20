using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CarServices carServices;
        private readonly UserManager<ApplicationUser> _userManager;

        public CarsController(ApplicationDbContext context, CarServices carServices, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            this.carServices=carServices;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
              return _context.Cars != null ? 
                          View(await _context.Cars.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            var carRequests = _context.CarRequests.Where(x => x.CarId.Equals(id)).ToList();
            this.ViewBag.CarRequests = carRequests;

            return View(car);
        }

        // GET: Cars/Create
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Make,Model,Year,NumPassengers,Description,PricePerDay")] Car car)
        {
            if (ModelState.IsValid)
            {
                await carServices.AddCar(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Reserve
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        //[ValidateAntiForgeryToken]
        public IActionResult Reserve(string id)
        {
            this.ViewBag.CarId = id;
            return View();
        }

        // Post: Cars/Reserve
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve([Bind("UserId, CarId, StartDate, EndDate")] CarRequest carRequest)
        {

            if (ModelState.IsValid)
            {
                    if(!carServices.IsOverlap(carRequest.StartDate, carRequest.EndDate))
                    {

                    carRequest.UserId = this._userManager.GetUserId(this.User)
;
                    _context.CarRequests.Add(carRequest);
                    }
                    else
                    {
                    return Problem("Dates Overlap With Another User");
                    }
                    await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Make,Model,Year,NumPassengers,Description,PricePerDay")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
            }
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool CarExists(string id)
        {
          return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
