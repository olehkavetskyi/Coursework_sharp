using Coursework.Entities;
namespace Coursework.CLI;

public class ProductMenu : Menu
{
    private readonly Database<Product> database;

    public ProductMenu()
    {
        database = Database<Product>.Instance;
    }

    protected override void DisplayMenu()
    {
        Console.WriteLine("Menu:");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. Update Product");
        Console.WriteLine("3. Remove Product");
        Console.WriteLine("4. Show All Products");
        Console.WriteLine("5. Search Product");
        Console.WriteLine("0. Exit");
    }

    protected override void ProcessChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                Console.Write("Enter the Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter the Price: ");
                float price = float.Parse(Console.ReadLine());
                Console.Write("Enter the Quantity: ");
                int quantity = int.Parse(Console.ReadLine());
                Console.Write("Enter the Type: ");
                string type = Console.ReadLine();

                Product product = new Product
                {
                    Id = GetNextId(),
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Type = type
                };

                database.Add(product);
                Console.WriteLine("Product added successfully.");
                break;
            case 2:
                Console.Write("Enter the ID of the product to update: ");
                int idToUpdate = int.Parse(Console.ReadLine());
                Product productToUpdate = database.GetById(idToUpdate);
                if (productToUpdate != null)
                {
                    Console.Write("Enter the updated Name: ");
                    productToUpdate.Name = Console.ReadLine();
                    Console.Write("Enter the updated Price: ");
                    productToUpdate.Price = float.Parse(Console.ReadLine());
                    Console.Write("Enter the updated Type: ");
                    productToUpdate.Type = Console.ReadLine();
                    Console.Write("Enter the updated Quantity: ");
                    productToUpdate.Quantity = int.Parse(Console.ReadLine());


                    database.Update(entry => entry.Id == productToUpdate.Id, entry => entry = productToUpdate);
                    Console.WriteLine("Product updated successfully.");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
                break;
            case 3:
                Console.Write("Enter the ID of the product to remove: ");
                int idToRemove = int.Parse(Console.ReadLine());
                database.Remove(e => GetIdValue(e) == idToRemove);
                Console.WriteLine("Product removed successfully.");
                break;
            case 4:
                ShowAllProducts();
                break;
            case 5:
                Console.Write("Enter the ID of the product to search: ");
                int idToSearch = int.Parse(Console.ReadLine());
                Product searchedProduct = database.GetById(idToSearch);
                if (searchedProduct != null)
                {
                    Console.WriteLine(searchedProduct);
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
                break;
            case 0:
                Console.WriteLine("Exiting...");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    private void ShowAllProducts()
    {
        List<Product> products = database.GetAll();
        foreach (Product product in products)
        {
            Console.WriteLine(product);
        }
    }

    private int GetIdValue(Product product)
    {
        return product.Id;
    }

    private int GetNextId()
    {
        List<Product> employees = database.GetAll();
        int maxId = 0;
        foreach (Product product in employees)
        {
            int productId = GetIdValue(product);
            if (productId > maxId)
            {
                maxId = productId;
            }
        }
        return maxId + 1;
    }
}
