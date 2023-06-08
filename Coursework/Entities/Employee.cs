namespace Coursework.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }
    public string Post { get; set; }

    private Employee()
    {
    }

    public class EmployeeBuilder
    {
        private Employee employee;

        public EmployeeBuilder()
        {
            employee = new Employee();
        }

        public EmployeeBuilder SetId(int id)
        {
            employee.Id = id;
            return this;
        }

        public EmployeeBuilder SetName(string name)
        {
            employee.Name = name;
            return this;
        }

        public EmployeeBuilder SetAge(int age)
        {
            employee.Age = age;
            return this;
        }

        public EmployeeBuilder SetSalary(decimal salary)
        {
            employee.Salary = salary;
            return this;
        }

        public EmployeeBuilder SetPost(string post)
        {
            employee.Post = post;
            return this;
        }

        public Employee Build()
        {
            return employee;
        }
    } 
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Age: {Age}, Salary: {Salary}, Post: {Post}";
    }
}

