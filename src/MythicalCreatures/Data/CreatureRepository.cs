using System.Text.Json;
using MythicalCreatures.Models;

namespace MythicalCreatures.Data;

public class CreatureRepository
{
    private const string FilePath = "creatures.json";
    private static readonly object _lock = new();

    public List<Creature> LoadCreatures()
    {
        if (!File.Exists(FilePath)) return new List<Creature>();

        lock (_lock)
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Creature>>(json) ?? new List<Creature>();
        }
    }

    public void SaveCreatures(List<Creature> creatures)
    {
        lock (_lock)
        {
            var json = JsonSerializer.Serialize(creatures, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}