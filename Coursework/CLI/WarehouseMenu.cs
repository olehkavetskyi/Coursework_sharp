using Coursework.Entities;

namespace Coursework.CLI;

public class WarehouseMenu : Menu
{
    private readonly Database<Warehouse> warehouseDatabase;
    private readonly Database<Condition> conditionDatabase;
    private readonly Database<Product> productDatabase;


    public WarehouseMenu()
    {
        warehouseDatabase = Database<Warehouse>.Instance;
        conditionDatabase = Database<Condition>.Instance;
        productDatabase = Database<Product>.Instance;
    }

    protected override void DisplayMenu()
    {
        Console.WriteLine("Warehouse Menu:");
        Console.WriteLine("1. Create Warehouse");
        Console.WriteLine("2. Add Product to Warehouse");
        Console.WriteLine("3. Remove Product from Warehouse");
        Console.WriteLine("4. Remove Warehouse");
        Console.WriteLine("5. Show All Warehouses");
        Console.WriteLine("6. Manage Warehouse Condition");
        Console.WriteLine("0. Exit");
    }

    protected override void ProcessChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                CreateWarehouse();
                break;
            case 2:
                AddProductToWarehouse();
                break;
            case 3:
                RemoveProductFromWarehouse();
                break;
            case 4:
                RemoveWarehouse();
                break;
            case 5:
                ShowAllWarehouses();
                break;
            case 6:
                ManageWarehouseCondition();
                break;
            case 0:
                Console.WriteLine("Exiting...");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    private void CreateWarehouse()
    {
        Console.Write("Enter the name of the warehouse: ");
        string name = Console.ReadLine();
        Console.Write("Enter the address of the warehouse: ");
        string address = Console.ReadLine();

        Console.Write("Select a temperature condition for the warehouse:");
        double temperature = double.Parse(Console.ReadLine());
        Console.Write("Select a humidity condition for the warehouse:");
        double humidity = double.Parse(Console.ReadLine());
        Condition selectedCondition = new Condition(temperature, humidity) { Id = GetNextConditionId()};

        Warehouse newWarehouse = new Warehouse { Id = GetNextWarehouseId(), Name = name, Address = address, Condition = selectedCondition };
        warehouseDatabase.Add(newWarehouse);
        conditionDatabase.Add(selectedCondition);
        Console.WriteLine("Warehouse created successfully.");
    }

    private void AddProductToWarehouse()
    {
        Console.Write("Enter the ID of the warehouse to add product: ");
#pragma warning disable CS8604 
        int warehouseId = int.Parse(Console.ReadLine());
        Warehouse warehouse = warehouseDatabase.GetById(warehouseId);

        if (warehouse != null)
        {
            Console.Write("Enter the name of the product: ");
            string productName = Console.ReadLine();
            Console.Write("Enter the price of the product: ");
            float productPrice = float.Parse(Console.ReadLine());
            Console.Write("Enter the quantity of the product: ");
            int productQuantity = int.Parse(Console.ReadLine());
            Console.Write("Enter the type of the product: ");
            string productType = Console.ReadLine();
#pragma warning restore CS8604
            Product newProduct = new Product { Id = GetNextProductId(), Quantity = productQuantity, Name = productName, Price = productPrice, Type = productType };
            warehouse.AddProduct(newProduct);
            productDatabase.Add(newProduct);
            warehouseDatabase.Update(x => x.Id == warehouseId, w => w = warehouse);
            Console.WriteLine("Product added to the warehouse successfully.");
        }
        else
        {
            Console.WriteLine("Warehouse not found.");
        }
    }

    private void RemoveProductFromWarehouse()
    {
        Console.Write("Enter the ID of the warehouse to remove product: ");
        int warehouseId = int.Parse(Console.ReadLine());
        Warehouse warehouse = warehouseDatabase.GetById(warehouseId);

        if (warehouse != null)
        {
            Console.Write("Enter the name of the product to remove: ");
            string productName = Console.ReadLine();
            Product product = warehouse.GetProductByName(productName);

            if (product != null)
            {
                warehouse.RemoveProduct(product);
                Console.WriteLine("Product removed from the warehouse successfully.");
            }
            else
            {
                Console.WriteLine("Product not found in the warehouse.");
            }
        }
        else
        {
            Console.WriteLine("Warehouse not found.");
        }
    }

    private void ShowAllWarehouses()
    {
        var warehouses = warehouseDatabase.GetAll();

        if (warehouses.Count == 0)
        {
            Console.WriteLine("No warehouses found.");
            return;
        }

        foreach (var warehouse in warehouses)
        {
            Console.WriteLine(warehouse);
            Console.WriteLine();
        }
    }

    private void ManageWarehouseCondition()
    {
        Console.Write("Enter the ID of the warehouse to manage condition: ");
        int warehouseId = int.Parse(Console.ReadLine());
        Warehouse warehouse = warehouseDatabase.GetById(warehouseId);

        if (warehouse != null)
        {
            Condition condition = warehouse.Condition;

            Console.WriteLine("Warehouse Condition:");
            Console.WriteLine($"Temperature: {condition.Temperature}°C");
            Console.WriteLine($"Humidity: {condition.Humidity}%");

            Console.WriteLine("\nCondition Menu:");
            Console.WriteLine("1. Update Temperature");
            Console.WriteLine("2. Update Humidity");
            Console.WriteLine("0. Back to Warehouse Menu");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the new temperature: ");
                    double newTemperature = double.Parse(Console.ReadLine());
                    condition.UpdateTemperature(newTemperature);
                    Console.WriteLine("Temperature updated successfully.");
                    break;
                case 2:
                    Console.Write("Enter the new humidity: ");
                    double newHumidity = double.Parse(Console.ReadLine());
                    condition.UpdateHumidity(newHumidity);
                    Console.WriteLine("Humidity updated successfully.");
                    break;
                case 0:
                    Console.WriteLine("Returning to Warehouse Menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Returning to Warehouse Menu...");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Warehouse not found.");
        }
    }

    private void RemoveWarehouse()
    {
        Console.Write("Enter the ID of the warehouse to remove: ");
        int warehouseId = int.Parse(Console.ReadLine());

        Warehouse warehouse = warehouseDatabase.GetById(warehouseId);
        if (warehouse != null)
        {
            warehouseDatabase.Remove(w => w.Id == warehouseId);
            conditionDatabase.Remove(w => w.Id == warehouse.Condition.Id);

            Console.WriteLine("Warehouse removed successfully.");
        }
        else
        {
            Console.WriteLine("Warehouse not found.");
        }
    }

    private int GetIdValue(Product product)
    {
        return product.Id;
    }

    private int GetNextProductId()
    {
        List<Product> employees = productDatabase.GetAll();
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

    private int GetIdValue(Warehouse warehouse)
    {
        return warehouse.Id;
    }

    private int GetNextWarehouseId()
    {
        List<Warehouse> warehouses = warehouseDatabase.GetAll();
        int maxId = 0;
        foreach (Warehouse warehouse in warehouses)
        {
            int warehouseId = GetIdValue(warehouse);
            if (warehouseId > maxId)
            {
                maxId = warehouseId;
            }
        }
        return maxId + 1;
    }
    private int GetIdValue(Condition condition)
    {
        return condition.Id;
    }

    private int GetNextConditionId()
    {
        List<Condition> conditions = conditionDatabase.GetAll();
        int maxId = 0;
        foreach (Condition condition in conditions)
        {
            int conditionId = GetIdValue(condition);
            if (conditionId > maxId)
            {
                maxId = conditionId;
            }
        }
        return maxId + 1;
    }
}
