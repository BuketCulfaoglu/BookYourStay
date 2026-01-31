using BookYourStay.Application.Common.Interfaces;
using BookYourStay.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookYourStayWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM()
            {
                VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenities"),
                Nights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(HomeVM homeVM)
        {
            homeVM.VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenities");
            foreach (var villa in homeVM.VillaList)
            {
                if (villa.Id % 2 == 0)
                {
                    villa.IsAvailable = false;
                }
            }
            return View(homeVM);
        }

        [HttpPost]
        public IActionResult GetVillasByDate(int nights, DateOnly checkInDate)
        {
            var villaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenities").ToList();

            foreach (var villa in villaList)
            {
                if (villa.Id % 2 == 0)
                {
                    villa.IsAvailable = false;
                }
            }

            HomeVM vm = new HomeVM()
            {
                VillaList = villaList,
                Nights = nights,
                CheckInDate = checkInDate
            };

            return PartialView("_VillaListPartial", vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}