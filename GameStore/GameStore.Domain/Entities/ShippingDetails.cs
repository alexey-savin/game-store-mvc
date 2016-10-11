using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Name must be specified")]
        public string Name { get; set; }

        [Required(ErrorMessage = "First address line must be specified")]
        [Display(Name = "First address")]
        public string Line1 { get; set; }

        [Display(Name = "Second address")]
        public string Line2 { get; set; }

        [Display(Name = "Third address")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "City must be specified")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country must be specified")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
