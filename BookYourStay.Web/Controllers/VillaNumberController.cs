using BookYourStay.Domain.Entities;
using BookYourStay.Infastructure.Data;
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
            var villaNumbers = _context.VillaNumbers.Include(u=>u.Villa).ToList();
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

                return RedirectToAction("Index", "VillaNumber");
            }
            return View();
        }


        public IActionResult Update(int villaId)
        {
            if (ModelState.IsValid)
            {
                Villa? updatedVilla = _context.Villas.FirstOrDefault((v => v.Id == villaId));

                if (updatedVilla != null)
                {
                    return View(updatedVilla);
                }
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult Update(Villa villa)
        {

            if (ModelState.IsValid)
            {
                _context.Villas.Update(villa);
                _context.SaveChanges();
                TempData["success"] = "The villa has been updated successfully.";

                return RedirectToAction("Index", "Villa");
            }
            return View();
        }


        public IActionResult Delete(int villaId)
        {
            Villa? villa = _context.Villas.FirstOrDefault(v => v.Id == villaId);
            if (villa != null)
            {
                return View(villa);
            }

            return RedirectToAction("Error", "Home");

        }

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            Villa? villaFromDb = _context.Villas.FirstOrDefault(v => v.Id == villa.Id);

            if (villaFromDb is not null)
            {
                _context.Villas.Remove(villaFromDb);
                _context.SaveChanges();
                TempData["success"] = "The villa has been deleted successfully.";

                return RedirectToAction("Index", "Villa");
            }

            TempData["error"] = "Failed to delete the villa.";

            return View();

        }
    }
}
