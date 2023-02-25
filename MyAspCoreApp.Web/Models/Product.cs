namespace MyAspCoreApp.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }

        public string Color { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsPublished { get; set; }

        public string Expire { get; set; }

        public string? ImagePath { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //[EmailAddress(ErrorMessage = "Email adresi uygun formatta değil.")]
        //public string EmailAdress { get; set; }

    }
}
