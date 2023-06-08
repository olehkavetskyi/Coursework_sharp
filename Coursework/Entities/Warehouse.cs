namespace Coursework.Entities;

public class Warehouse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required Condition Condition { get; set; }
    public List<Product> Products { get; set; } 
    public Warehouse() 
    {
        Products= new List<Product>();
    }
    public Warehouse(string name, string address, Condition condition)
    {
        Name = name;
        Address = address;
        Condition = condition;
        Products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public void RemoveProduct(Product product)
    {
        Products.Remove(product);
    }

    public Product GetProductByName(string name)
    {
        return Products.Find(p => p.Name == name);
    }

    public override string ToString()
    {
        return $"Warehouse: {Name}\nAddress: {Address}\nCondition: {Condition}";
    }
}
