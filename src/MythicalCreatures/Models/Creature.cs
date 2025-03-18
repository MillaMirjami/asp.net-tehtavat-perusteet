using System.Text.Json.Serialization;

namespace MythicalCreatures.Models;

public class Creature
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
}