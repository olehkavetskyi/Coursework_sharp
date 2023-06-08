using Coursework.Entities;

namespace Coursework.CLI;

public class CorporationMenu : Menu
{
    private readonly Database<Corporation> corporationDatabase;

    public CorporationMenu()
    {
        corporationDatabase = Database<Corporation>.Instance;
    }


    protected override void DisplayMenu()
    {
        Console.WriteLine("Corporation Menu:");
        Console.WriteLine("1. Create Corporation");
        Console.WriteLine("2. Update Corporation");
        Console.WriteLine("3. Remove Corporation");
        Console.WriteLine("4. Show All Corporations");
        Console.WriteLine("0. Exit");
    }

    protected override void ProcessChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                CreateCorporation();
                break;
            case 2:
                UpdateCorporation();
                break;
            case 3:
                RemoveCorporation();
                break;
            case 4:
                ShowAllCorporations();
                break;
            case 0:
                Console.WriteLine("Exiting Corporation Menu...");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    private void CreateCorporation()
    {
        Console.Write("Enter the name of the corporation: ");
        string name = Console.ReadLine();
        Console.Write("Enter the address of the corporation: ");
        string address = Console.ReadLine();
        Console.Write("Enter the power of the corporation: ");
        int power = int.Parse(Console.ReadLine());

        Corporation corporation = new Corporation
        {
            Id = GetNextCorporationId(),
            Name = name,
            Address = address,
            Power = power
        };

        corporationDatabase.Add(corporation);
        Console.WriteLine("Corporation created successfully.");
    }

    private void UpdateCorporation()
    {
        Console.Write("Enter the ID of the corporation to update: ");
        int idToUpdate = int.Parse(Console.ReadLine());
        Corporation corporationToUpdate = corporationDatabase.GetById(idToUpdate);
        if (corporationToUpdate != null)
        {
            Console.Write("Enter the updated name of the corporation: ");
            corporationToUpdate.Name = Console.ReadLine();
            Console.Write("Enter the updated address of the corporation: ");
            corporationToUpdate.Address = Console.ReadLine();
            Console.Write("Enter the updated power of the corporation: ");
            corporationToUpdate.Power = int.Parse(Console.ReadLine());

            corporationDatabase.Update(entry => entry.Id == corporationToUpdate.Id, entry => entry = corporationToUpdate);
            Console.WriteLine("Corporation updated successfully.");
        }
        else
        {
            Console.WriteLine("Corporation not found.");
        }
    }

    private void RemoveCorporation()
    {
        Console.Write("Enter the ID of the corporation to remove: ");
        int idToRemove = int.Parse(Console.ReadLine());
        corporationDatabase.Remove(c => c.Id == idToRemove);
        Console.WriteLine("Corporation removed successfully.");
    }

    private void ShowAllCorporations()
    {
        var corporations = corporationDatabase.GetAll();
        foreach (var corporation in corporations)
        {
            Console.WriteLine(corporation);
        }
    }

    private int GetNextCorporationId()
    {
        var corporations = corporationDatabase.GetAll();
        int maxId = 0;
        foreach (var corporation in corporations)
        {
            if (corporation.Id > maxId)
                maxId = corporation.Id;
        }
        return maxId + 1;
    }
}

