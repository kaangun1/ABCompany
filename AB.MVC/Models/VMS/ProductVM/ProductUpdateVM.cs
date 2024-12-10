﻿using AB.Entities.Models.Concrete;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AB.MVC.Models.VMS.ProductVM
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Urun Adi zorunlu alandir")]
        [DisplayName("Urun Adi")]
        [MaxLength(50,ErrorMessage ="En fazla 50 karakter olabilir")]
        [MinLength(3,ErrorMessage ="En az 3 karakter olmalidir")]
        public string ProductName { get; set; }




        [Required(AllowEmptyStrings =false,ErrorMessage ="Urun Kodu zorunlu alandir")]
        [DisplayName("Urun Kodu")]
        [MaxLength(50, ErrorMessage = "En fazla 50 karakter olabilir")]
        [MinLength(3, ErrorMessage = "En az 3 karakter olmalidir")]
        public string ProductCode { get; set; }

        public string?  Description { get; set; }

        [DisplayName("Urun Adedi")]
        //[MinLength(1, ErrorMessage = "0 dan buyuk olmalidir")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [DisplayName("Urun Fiyati")]
        //[MinLength(1m, ErrorMessage = "0 dan buyuk olmalidir")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        public List<ProductInCategory?> Categories { get; set; } = new List<ProductInCategory?>();
        public List<Unite>? Unites { get; set; } = new List<Unite>();
        public int UniteId { get; set; }
    }
}