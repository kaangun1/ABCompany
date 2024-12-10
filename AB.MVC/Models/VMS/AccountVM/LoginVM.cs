using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AB.MVC.Models.VMS.AccountVM
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Email Alani Zorunludur")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Şifre Zorunlu Alandir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Beni Hatirla")]
        public bool RememberMe { get; set; }
    }
}
