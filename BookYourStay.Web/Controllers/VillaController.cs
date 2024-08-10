using BookYourStay.Domain.Entities;
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
                ModelState.AddModelError("","The Description cannot exactly match the Name.");      //validation-summary
                ModelState.AddModelError("name","The Description cannot exactly match the Name.");  //name property
            }

            if (ModelState.IsValid)
            {
                _context.Villas.Add(villa);
                _context.SaveChanges();

                return RedirectToAction("Index", "Villa");
            }
            return View();
        }
    }
}
