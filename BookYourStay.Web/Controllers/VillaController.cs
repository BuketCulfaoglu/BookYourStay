﻿using BookYourStay.Domain.Entities;
using BookYourStay.Infastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookYourStay.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var villas = _context.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            //Custom Validation
            if (villa.Name.Equals(villa.Description))
            {
                ModelState.AddModelError("", "The Description cannot exactly match the Name.");      //validation-summary
                ModelState.AddModelError("name", "The Description cannot exactly match the Name.");  //name property
            }

            if (ModelState.IsValid)
            {
                _context.Villas.Add(villa);
                _context.SaveChanges();
                TempData["success"] = "The villa has been created successfully.";

                return RedirectToAction(nameof(Index));
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

                return RedirectToAction(nameof(Index));
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

                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Failed to delete the villa.";

            return View();

        }
    }
}
