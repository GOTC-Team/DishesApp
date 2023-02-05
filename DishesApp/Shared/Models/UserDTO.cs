using System.ComponentModel.DataAnnotations;

namespace DishesApp.Shared.Models
{
    public class UserDTO
    {
        [Display(Name = "Name")]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Password must be > {2} and < {1}", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string? Password { get; set; }    
    }
}
