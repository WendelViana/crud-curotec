namespace crud_curotec.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void Update(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
