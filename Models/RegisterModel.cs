using System.ComponentModel.DataAnnotations;

namespace stokEnka.Models
{
    public class RegisterModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string ContactPerson { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string TaxNumber { get; set; }

        [Required]
        public string AuctionItem { get; set; }
    }
}
