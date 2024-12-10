using System.ComponentModel.DataAnnotations;

namespace AB.MVC.Models.VMS.AccountVM
{
    public class EditVM
    {
        public int Id { get; set; }


        [MinLength(2,ErrorMessage ="Isim En az 2 karakterli olmalidir")]
        public string Ad { get; set; }
        
        [MinLength(2, ErrorMessage = "Soyad En az 2 karakterli olmalidir")]
        public string Soyad { get; set; }

        [MinLength(10, ErrorMessage = "Gsm En az 10 karakterli olmalidir")]
        public string Gsm { get; set; }


        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email Adresi Zorunludur")]
        public string Email { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre  Zorunludur")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre  Zorunludur")]
        [Compare("Password",ErrorMessage ="Şifreler Uyumsuzdur")]
        public string RePassword { get; set; }
    }
}
