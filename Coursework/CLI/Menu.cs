namespace Coursework.CLI;

public abstract class Menu
{
    public void Run()
    {
        int choice;
        do
        {
            Console.Clear();
            DisplayMenu();
            Console.Write("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            Console.Clear(); 
            ProcessChoice(choice);

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        } while (choice != 0);
    }

    protected abstract void DisplayMenu();
    protected abstract void ProcessChoice(int choice);
}
