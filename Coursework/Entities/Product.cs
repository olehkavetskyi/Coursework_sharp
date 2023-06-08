namespace Coursework.Entities;

public class Product
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required float Price { get; set; }
    public required int Quantity { get; set; }
    public required string Type { get; set; }

    public Product() { }
    public Product(string name, float price, int quantity, string type)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        Type = type;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: {Price:C}, Quantity: {Quantity}, Type: {Type}";
    }
}
