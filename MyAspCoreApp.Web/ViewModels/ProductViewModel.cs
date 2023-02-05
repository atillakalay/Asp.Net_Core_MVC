﻿namespace MyAspCoreApp.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string? Color { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public string Expire { get; set; }
    }
}
