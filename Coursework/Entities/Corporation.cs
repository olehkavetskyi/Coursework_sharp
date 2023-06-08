namespace Coursework.Entities;

public class Corporation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Power { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Address: {Address}, Powel: {Power}";
    }
}
