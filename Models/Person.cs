using System.ComponentModel.DataAnnotations;

namespace stokEnka.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }

    public class User : Person
    {
        // User-specific fields can be added here if needed
    }

    public class Company : Person
    {
        [Required]
        [Display(Name = "Şirket Adı")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Vergi Numarası")]
        public string TaxNumber { get; set; }

        [Display(Name = "İlgili Kişi")]
        public string ContactPerson { get; set; }
    }
}
