using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

public class DataSerializer<T>
{
    public void SerializeBinary(T data, string fileName)
    {
        try
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сериализации в бинарном формате: {ex.Message}");
        }
    }

    public T DeserializeBinary(string fileName)
    {
        try
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(fs);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при десериализации из бинарного формата: {ex.Message}");
            return default;
        }
    }

    public void SerializeJson(T data, string fileName)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"Данные успешно сохранены в JSON файл: {fileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сериализации в JSON: {ex.Message}");
        }
    }

    public T DeserializeJson(string fileName)
    {
        try
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<T>(jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при десериализации из JSON: {ex.Message}");
            return default;
        }
    }
}