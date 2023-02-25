using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyAspCoreApp.Web.ViewModels
{
    public class ProductUpdateViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "İsim alanı boş olamaz.")]
        [StringLength(50, ErrorMessage = "50 Karekterden fazla giriş yapılamaz.")]
        [Remote(action: "HasProductName", controller: "Product")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fiyat alanı boş olamaz.")]
        [Range(1, 200, ErrorMessage = "Girdiniz değerlerin 1 ila 1000 arasında olması gerekmektedir.")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Stok alanı boş olamaz")]
        [Range(1, 200, ErrorMessage = "Girdiniz değerlerin 1 ila 200 arasında olması gerekmektedir.")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Açıklama alanı boş olamaz.")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Açıklama uzunluğu 10 ila 100 arasında olması gerekmektedir.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Renk alanı boş olamaz.")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Yayın Tarihi alanı boş olamaz.")]
        public DateTime? PublishDate { get; set; }

        [Required(ErrorMessage = "Yayın izin alanı boş olamaz.")]
        public bool IsPublished { get; set; }
        [Required(ErrorMessage = "Yayın süresi alanı boş olamaz.")]
        public string Expire { get; set; }
        [ValidateNever]
        public IFormFile? Image { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }
    }
}
