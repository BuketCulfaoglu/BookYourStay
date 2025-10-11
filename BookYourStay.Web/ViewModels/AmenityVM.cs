using BookYourStay.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookYourStay.Web.ViewModels
{
    public class AmenityVM
    {
        public Amenity? Amenity { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? AmenityList { get; set; }
    }
}
