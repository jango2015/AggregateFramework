namespace App.Core.Products
{
    public class AddProduct
    {
        public AddProduct(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public string Name { get; private set; }
        public string Description { get; private  set; }
        public decimal Price { get; private set; }
    }
}
