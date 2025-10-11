using BookYourStay.Application.Common.Interfaces;
using BookYourStay.Domain.Entities;
using BookYourStay.Infrastructure.Data;
using BookYourStay.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookYourStay.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }

        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                AmenityList = _unitOfWork.Villa.GetAll().ToList().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Create(Amenity amenity)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Add(amenity);
                _unitOfWork.Save();
                TempData["success"] = "The amenity has been created successfully.";


                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public IActionResult Update(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                AmenityList = _unitOfWork.Amenity.GetAll().ToList().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };

            if (amenityVM.Amenity == null)
                return RedirectToAction("Error", "Home");

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(Amenity amenity)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Update(amenity);
                _unitOfWork.Save();
                TempData["success"] = "The amenity has been updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                AmenityList = _unitOfWork.Villa.GetAll().ToList().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };

            if (amenityVM.Amenity == null)
                return RedirectToAction("Error", "Home");

            return View(amenityVM);

        }

        [HttpPost]
        public IActionResult Delete(Amenity amenity)
        {

            Amenity? amenityFromDb = _unitOfWork.Amenity.Get(v => v.Id == amenity.Id);

            if (amenityFromDb is not null)
            {
                _unitOfWork.Amenity.Remove(amenityFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The amenity has been deleted successfully.";

                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Failed to delete the amenity.";

            return View();

        }
    }
}
