using BookYourStay.Domain.Entities;
using BookYourStay.Infrastructure.Data;
using BookYourStay.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookYourStay.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillaNumberController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var villaNumbers = _context.VillaNumbers.Include(u => u.Villa).ToList();
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumber villaNumber)
        {
            if (ModelState.IsValid)
            {
                bool villaExists = _context.VillaNumbers.Find(villaNumber.Villa_Number) != null;
                //bool villaNumberExists = _context.VillaNumbers.Any(u => u.Villa_Number == villaNumber.Villa_Number);
                if (villaExists)
                {
                    TempData["error"] = "The villa number already exists.";
                }
                else
                {
                    _context.VillaNumbers.Add(villaNumber);
                    _context.SaveChanges();
                    TempData["success"] = "The villa number has been created successfully.";
                }

                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                VillaNumber = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
            };

            if (villaNumberVM.VillaNumber == null)
                return RedirectToAction("Error", "Home");

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumber villaNumber)
        {
            if (ModelState.IsValid)
            {
                _context.VillaNumbers.Update(villaNumber);
                _context.SaveChanges();
                TempData["success"] = "The villa number has been updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                VillaNumber = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
            };

            if (villaNumberVM.VillaNumber == null)
                return RedirectToAction("Error", "Home");

            return View(villaNumberVM);

        }

        [HttpPost]
        public IActionResult Delete(VillaNumber villaNumber)
        {
  
            VillaNumber? villaNumberFromDb = _context.VillaNumbers.FirstOrDefault(v => v.Villa_Number == villaNumber.Villa_Number);

            if (villaNumberFromDb is not null)
            {
                _context.VillaNumbers.Remove(villaNumberFromDb);
                _context.SaveChanges();
                TempData["success"] = "The villa number has been deleted successfully.";

                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Failed to delete the villa number.";

            return View();

        }
    }
}
