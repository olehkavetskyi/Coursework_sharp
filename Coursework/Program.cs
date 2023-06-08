using Coursework.CLI;

namespace Coursework;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Application!");

        var employeeMenu = new EmployeeMenu();
        var productMenu = new ProductMenu();
        var warehouseMenu = new WarehouseMenu();
        var corporationMenu = new CorporationMenu();


        int choice;
        do
        {
            Console.Clear(); 
            DisplayMainMenu();
            Console.Write("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            Console.Clear(); 
            switch (choice)
            {
                case 1:
                    employeeMenu.Run();
                    break;
                case 2:
                    productMenu.Run();
                    break;
                case 3:
                    warehouseMenu.Run();
                    break;
                case 4:
                    corporationMenu.Run();
                    break;
                case 0:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        } while (choice != 0);
    }

    private static void DisplayMainMenu()
    {
        Console.WriteLine("Main Menu:");
        Console.WriteLine("1. Employee Menu");
        Console.WriteLine("2. Product Menu");
        Console.WriteLine("3. Warehouse Menu");
        Console.WriteLine("4. Corporation Menu");
        Console.WriteLine("0. Exit");
    }
}
