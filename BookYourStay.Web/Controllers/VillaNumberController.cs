using BookYourStay.Application.Common.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var villaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties: "Villa");
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().ToList().Select(u => new SelectListItem()
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
                bool villaExists = _unitOfWork.VillaNumber.Any(u => u.Villa_Number == villaNumber.Villa_Number);
                if (villaExists)
                {
                    TempData["error"] = "The villa number already exists.";
                }
                else
                {
                    _unitOfWork.VillaNumber.Add(villaNumber);
                    _unitOfWork.Save();
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
                VillaList = _unitOfWork.Villa.GetAll().ToList().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)
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
                _unitOfWork.VillaNumber.Update(villaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa number has been updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().ToList().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)
            };

            if (villaNumberVM.VillaNumber == null)
                return RedirectToAction("Error", "Home");

            return View(villaNumberVM);

        }

        [HttpPost]
        public IActionResult Delete(VillaNumber villaNumber)
        {

            VillaNumber? villaNumberFromDb = _unitOfWork.VillaNumber.Get(v => v.Villa_Number == villaNumber.Villa_Number);

            if (villaNumberFromDb is not null)
            {
                _unitOfWork.VillaNumber.Remove(villaNumberFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The villa number has been deleted successfully.";

                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Failed to delete the villa number.";

            return View();

        }
    }
}
