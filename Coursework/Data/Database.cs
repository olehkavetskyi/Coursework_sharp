using System.Reflection;
using Newtonsoft.Json;

public class Database<T>
{
    private readonly string filePath;
    private List<T> entries;
    private static Database<T> instance;

    public static Database<T> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Database<T>();
            }
            return instance;
        }
    }

    private Database()
    {
        filePath = $"{typeof(T).Name.ToLower()}.json";
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            entries = JsonConvert.DeserializeObject<List<T>>(jsonData);
        }
        else
        {
            entries = new List<T>();
        }
    }

    private void SaveData()
    {
        string jsonData = JsonConvert.SerializeObject(entries, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }

    public void Add(T entry)
    {
        entries.Add(entry);
        SaveData();
    }

    public void Update(Predicate<T> predicate, Action<T> updateAction)
    {
        T entryToUpdate = entries.Find(predicate);
        if (entryToUpdate != null)
        {
            updateAction(entryToUpdate);
            SaveData();
        }
    }

    public void Remove(Predicate<T> predicate)
    {
        T entryToRemove = entries.Find(predicate);
        if (entryToRemove != null)
        {
            entries.Remove(entryToRemove);
            SaveData();
        }
    }

    public T GetById(int id)
    {
        return entries.Find(e => GetIdValue(e) == id);
    }

    public List<T> GetAll()
    {
        return entries;
    }

    private int GetIdValue(T entry)
    {
        PropertyInfo idProperty = typeof(T).GetProperty("Id");
        if (idProperty != null && idProperty.PropertyType == typeof(int))
        {
            return (int)idProperty.GetValue(entry);
        }
        else
        {
            throw new InvalidOperationException("Invalid ID property");
        }
    }

}
