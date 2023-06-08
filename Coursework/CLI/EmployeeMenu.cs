using Coursework.Entities;

namespace Coursework.CLI;

public class EmployeeMenu : Menu
{
    private readonly Database<Employee> database;

    public EmployeeMenu()
    {
        database = Database<Employee>.Instance;
    }

    protected override void DisplayMenu()
    {
        Console.WriteLine("Menu:");
        Console.WriteLine("1. Add Employee");
        Console.WriteLine("2. Update Employee");
        Console.WriteLine("3. Remove Employee");
        Console.WriteLine("4. Show All Employees");
        Console.WriteLine("5. Search Employee");
        Console.WriteLine("0. Exit");
    }

    protected override void ProcessChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                Console.Write("Enter the Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter the Age: ");
                int age = int.Parse(Console.ReadLine());
                Console.Write("Enter the Salary: ");
                decimal salary = decimal.Parse(Console.ReadLine());
                Console.Write("Enter the Post: ");
                string post = Console.ReadLine();

                Employee employee = new Employee
                    .EmployeeBuilder()
                    .SetId(GetNextId())
                    .SetName(name)
                    .SetAge(age)
                    .SetPost(post)
                    .SetSalary(salary)
                    .Build();

                database.Add(employee);
                Console.WriteLine("Employee added successfully.");
                break;
            case 2:
                Console.Write("Enter the ID of the employee to update: ");
                int idToUpdate = int.Parse(Console.ReadLine());
                Employee employeeToUpdate = database.GetById(idToUpdate);
                if (employeeToUpdate != null)
                {
                    Console.Write("Enter the updated Name: ");
                    employeeToUpdate.Name = Console.ReadLine();
                    Console.Write("Enter the updated Age: ");
                    employeeToUpdate.Age = int.Parse(Console.ReadLine());
                    Console.Write("Enter the updated Salary: ");
                    employeeToUpdate.Salary = decimal.Parse(Console.ReadLine());

                    database.Update(entry => entry.Id == employeeToUpdate.Id, entry => entry = employeeToUpdate);
                    Console.WriteLine("Employee updated successfully.");
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
                break;
            case 3:
                Console.Write("Enter the ID of the employee to remove: ");
                int idToRemove = int.Parse(Console.ReadLine());
                database.Remove(e => GetIdValue(e) == idToRemove);
                Console.WriteLine("Employee removed successfully.");
                break;
            case 4:
                ShowAllEmployees();
                break;
            case 5:
                Console.Write("Enter the ID of the employee to search: ");
                int idToSearch = int.Parse(Console.ReadLine());
                Employee searchedEmployee = database.GetById(idToSearch);
                if (searchedEmployee != null)
                {
                    Console.WriteLine(searchedEmployee);
                }
                else
                {
                    Console.WriteLine("Employee not found.");
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

    private void ShowAllEmployees()
    {
        List<Employee> employees = database.GetAll();
        foreach (Employee employee in employees)
        {
            Console.WriteLine(employee);
        }
    }

    private int GetNextId()
    {
        List<Employee> employees = database.GetAll();
        int maxId = 0;
        foreach (Employee employee in employees)
        {
            int employeeId = GetIdValue(employee);
            if (employeeId > maxId)
            {
                maxId = employeeId;
            }
        }
        return maxId + 1;
    }

    private int GetIdValue(Employee employee)
    {
        return employee.Id;
    }
}