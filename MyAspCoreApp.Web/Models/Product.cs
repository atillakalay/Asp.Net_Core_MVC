using System.ComponentModel.DataAnnotations;

namespace MyAspCoreApp.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim alanı boş olamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Fiyat alanı boş olamaz")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Stok alanı boş olamaz")]
        public int? Stock { get; set; }
        [Required(ErrorMessage = "Açıklama alanı boş olamaz")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Renk alanı boş olamaz")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Yayın Tarihi alanı boş olamaz")]
        public DateTime? PublishDate { get; set; }
        [Required(ErrorMessage = "Yayın izin alanı boş olamaz")]
        public bool? IsPublished { get; set; }
        [Required(ErrorMessage = "Yayın süresi alanı boş olamaz")]
        public string? Expire { get; set; }
    }
}
