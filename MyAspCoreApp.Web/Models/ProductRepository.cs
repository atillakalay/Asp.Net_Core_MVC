namespace MyAspCoreApp.Web.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>
        {
            new Product() { Id = 1, Name = "Kalem", Price = 15, Stock = 750 },
            new Product() { Id = 2, Name = "Silgi", Price = 25, Stock = 500 },
            new Product() { Id = 3, Name = "Kalem Kutusu", Price = 35, Stock = 250 }
        };



        public List<Product> GetAll() => _products;
        public void Add(Product newProduct) => _products.Add(newProduct);

        public void Remove(int id)
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == id);
            if (hasProduct == null)
            {
                throw new Exception($"Bu Id({id})' ye sahip ürün bulunamamaktadır.");
            }

            _products.Remove(hasProduct);
        }

        public void Update(Product updateProduct)
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == updateProduct.Id);
            if (hasProduct == null)
            {
                throw new Exception($"Bu Id({updateProduct.Id})' ye sahip ürün bulunamamaktadır.");
            }

            hasProduct.Name = updateProduct.Name;
            hasProduct.Price = updateProduct.Price;
            hasProduct.Stock = updateProduct.Stock;

            var index = _products.FindIndex(x => x.Id == updateProduct.Id);
            _products[index] = hasProduct;
        }
    }
}
